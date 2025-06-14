using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Infrastructure.Controllers;
using Google.Cloud.AIPlatform.V1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Value = Google.Protobuf.WellKnownTypes.Value;
using MoneyOrbit.Core.Entities;
using MoneyOrbit.Application.DTOs.GoalDtos;
using System.Text;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;

namespace MoneyOrbit.Infrastructure.Services
{
    public class AiService:IAiService
    {
        private readonly string _projectId;
        private readonly string _location;
        private readonly string _modelId = "gemini-2.5-flash";

        private readonly IGoalService _goalService;
        private readonly ITransactionRepository<ITransaction> _transactionRepository;

        public AiService(IGoalService goalService, ITransactionRepository<ITransaction> transactionRepository)
        {
            _projectId = Environment.GetEnvironmentVariable("Google_Cloud_Project_ID");
            _location = Environment.GetEnvironmentVariable("Google_Cloud_Project_Location");
            this._goalService = goalService;
            this._transactionRepository = transactionRepository;
        }

        private async Task<string> GeneratePrompt(GetGoalDto getGoalDto)
        {
            Goal goal =(Goal)(await _goalService.GetGoal(getGoalDto)).Result;

            //we need date accDebited, accCredit, amount

            //for loop to get this info for each transactionID
            //populate prompt with these transactions

            // Building the string that will be sent to the AI
            var promptBuilder = new StringBuilder();

            // Setting the persona and context for the AI
            promptBuilder.AppendLine("You are a friendly budgeting assistant who offers encouraging, actionable advice to help users improve their finances and reach their goals.");
            promptBuilder.AppendLine("Analyze the following description of the user's goal for a specific account, then provide feedback.");
            promptBuilder.AppendLine("---");

            // Providing details of the user's budget
            promptBuilder.AppendLine($"Goal Name: {goal.GoalName}");
            promptBuilder.AppendLine($"Goal Description: {goal.GoalDescription}");
            promptBuilder.AppendLine($"Total amount that has been accomplished towards the goal: {goal.AmountAccomplished}");
            promptBuilder.AppendLine($"Current amount that is stored in the account: {goal.Amount}\n");

            // Providing details for each transaction related to the goal
            promptBuilder.AppendLine("These are the transactions of the account that are related to the goal: \n");
            foreach (var transactionID in goal.RelatedTransactionIDs)
            {
                var tr = await _transactionRepository.GetInstanceOfType<Transaction>(transactionID);
                promptBuilder.AppendLine($"date{tr.Date}, account credited{tr.AccCreditedID}," +
                $"account credited{tr.AccDebitedID}, amount {tr.Amount}");
            }

            // Giving the task to the AI
            promptBuilder.AppendLine("**Your Task**");
            promptBuilder.AppendLine("Give finanical advise based on the transacitons.");
            


            //return the string that it wants
            return promptBuilder.ToString();
        }
        public async Task<string> GetAdvice(GetGoalDto getGoalDto)
        {

            string prompt = await GeneratePrompt(getGoalDto);

            var predictionServiceClient = new PredictionServiceClientBuilder
            {
                Endpoint = $"{_location}-aiplatform.googleapis.com"
            }.Build();

            var endpoint = EndpointName.FromProjectLocationPublisherModel(_projectId, _location, "google", _modelId);

            var instance = new Value
            {
                StructValue = new Struct { Fields = { { "prompt", Value.ForString(prompt) } } }
            }; ;

            var parameters = new Value
            {
                StructValue = new Struct
                {
                    Fields =
                    {
                        { "temperature", Value.ForNumber(0.3) },
                        { "maxOutputTokens", Value.ForNumber(1024) },
                        { "topP", Value.ForNumber(0.8) },
                        { "topK", Value.ForNumber(40) }
                    }
                }
            };

            var response = await predictionServiceClient.PredictAsync(endpoint, new[] { instance }, parameters);

            var prediction = response.Predictions.FirstOrDefault();
            var generatedText = prediction?.StructValue?.Fields["content"]?.StringValue ?? "Sorry, I couldn't generate any advice at this moment.";

            return generatedText;


        }
    }
}