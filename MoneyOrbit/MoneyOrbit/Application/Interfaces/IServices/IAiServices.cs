using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IAiService
    {
        /// <summary>
        /// This method generates a prompt and sends it to the Gemini agent and
        /// returns the response.
        /// </summary>
        /// <returns>The response from Gemini</returns>
        Task<string> GetAdvice(GetGoalDto getGoalDto);
    }
}