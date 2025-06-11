using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    internal interface IGoalFactory<TGoal>:IFactory<TGoal> where TGoal : IGoal
    {
        /// <summary>
        /// Creates a new goal with the specified parameters. It should generate the id and give it the date.
        /// </summary>
        /// <param name="goalName">The name of the goal.</param>
        /// <param name="amount">The target amount for the goal.</param>
        /// <param name="accDebitedID"> The ID of the account that will be debited for this goal. eg Xbox, Student Loan</param>
        /// <param name="userID"> The ID of the user who owns the goal.</param>
        /// <returns>A new instance of a goal.</returns>
        Goal CreateGoal(string goalName, string goalDescription, string accDebitedID, decimal amount, DateTime? date);
    }
}
