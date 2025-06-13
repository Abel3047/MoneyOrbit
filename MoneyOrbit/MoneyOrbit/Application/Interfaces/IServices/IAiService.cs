using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IAiService
    {
        /// <summary>
        /// This method retrieves all the relevant information about the user's accounts and goals 
        /// and returns a prompt that will be sent to the Gemini agent.
        /// </summary>
        /// <returns>The prompt that will be sent to Gemini</returns>
        Task<string> GeneratePrompt();

        /// <summary>
        /// This method takes in a prompt and sends it to the Gemini agent and
        /// returns the response.
        /// </summary>
        /// <param name="prompt">The prompt that will be sent to Gemini</param>
        /// <returns>The response from Gemini</returns>
        Task<string> GetResponse(string prompt);

        /// <summary>
        /// This method will take in user feedback about the AI and relays that information
        /// to the Gemini agent.
        /// </summary>
        /// <param name="feedback">The feedback from the user</param>
        /// <returns></returns>
        Task GiveFeedback(string feedback);
    }
}