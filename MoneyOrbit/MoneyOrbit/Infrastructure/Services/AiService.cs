using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;
using Google.Cloud.AIPlatform.V1;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Value = Google.Protobuf.WellKnownTypes.Value;

namespace MoneyOrbit.Infrastructure.Services
{
    public class AiServices:IAiService
    {
        private readonly string _projectId;
        private readonly string _location;
        private readonly string _modelId = "gemini-2.5-flash";

        private IGoal _goal;

        public AiServices(string projectId, string location, IGoal goal)
        {
            _projectId = projectId;
            _location = location;
            _goal = goal; 
        }

        public Task<string> GeneratePrompt(IGoal _goal)
        {
            var goalDescription = _goal.Description;
            var goalName = _goal.GoalName;


        }
        public async Task<string> GetResponse()
        {

            string prompt = GeneratePrompt();

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