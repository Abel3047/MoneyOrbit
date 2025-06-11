using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IGoalService
    {
        Task<ResultObject> AssignTransationToGoal(AssignTransationToGoalDto assignTransationToGoalDto);
        /// <summary>
        /// This methods creates a goal in the database, and returns a ResultObject with the result of the operation.
        /// <para>This requires the <param name="goalCreationDto">GoalCreationDto</param> to have the following properties:
        /// a valid AccDebitedID, a valid Amount, a valid GoalName, and a valid Date.
        /// </para>
        /// <para> Its particularly important for AccDebitedID to be there because thats what we will be using to index and find
        /// goals and transactions</para>
        /// </summary>
        /// <param name="goalCreationDto"></param>
        /// <returns></returns>
        Task<ResultObject> CreateGoal(GoalCreationDto goalCreationDto);
        /// <summary>
        /// This method updates a goal in the database, and returns a ResultObject with the result of the operation.
        /// <para> This requires ID of the goal you want to update, and any of the goals properties expect the AccDebited.
        /// If the provided property is null/empty/0/less than 0, it will keep the value from the database</para>
        /// </summary>
        /// <param name="goalUpdateDto"></param>
        /// <returns></returns>
        Task<ResultObject> UpdateGoal(GoalUpdateDto goalUpdateDto);
    }
}