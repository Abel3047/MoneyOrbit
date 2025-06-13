using MoneyOrbit.Application.Interfaces.IEntities;

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
        /// This method generates a prompt and sends it to the Gemini agent and
        /// returns the response.
        /// </summary>
        /// <returns>The response from Gemini</returns>
        Task<string> GetResponse();
    }
}