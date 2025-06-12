using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Core.Entities;
using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IGoalService
    {
        /// <summary>
        /// This method assigns a transaction to a goal in the database, and returns a ResultObject with the result of the operation.
        /// <para> It does this by taking a goal and transaction ID, then checks to see if the transaction relates to the goal with
        /// <see cref="Goal.RelatesToTransaction{IGoal}(IEntities.ITransaction)"/></para>. If it does it will update the goal with 
        /// the transaction ID, and add the amount of the transaction to the goal's AmountAccomplished property.
        /// </summary>
        /// <param name="assignTransationToGoalDto"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This is simply a method that exposes the amountacomplished property of the goal entity.
        /// </summary>
        /// <param name="gAADto"></param>
        /// <returns></returns>
        Task<ResultObject> GetGoalAmountAccomplished(GoalAmountAccomplishedDto gAADto);
        /// <summary>
        /// This method retrieves a goal from the database based on the provided GoalID found in the <paramref name="getGoalDto"/>.
        /// </summary>
        /// <param name="getGoalDto"></param>
        /// <returns></returns>
        Task<ResultObject> GetGoal(GetGoalDto getGoalDto);
        /// <summary>
        /// This gets the userID, fines the goals associated with the user and returns a ResultObject with the goals.
        /// <para>It works a lot like <see cref="ITransactionService.GetUserTransactions(DTOs.TransactionDtos.GetTransactionDto)"/>
        /// but we can't inhert it because the services are too different, save for this one method</para>
        /// </summary>
        /// <param name="getGoalsForUserDto"></param>
        /// <returns></returns>
        Task<ResultObject> GetGoalsForUser(GetGoalsForUserDto getGoalsForUserDto);
    }
}