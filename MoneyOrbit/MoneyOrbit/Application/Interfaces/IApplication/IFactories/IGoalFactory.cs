using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    internal interface IGoalFactory<TGoal>:IFactory<TGoal> where TGoal : IGoal
    {
        /// <summary>
        /// Creates a new goal with the specified parameters. It should generate the id and give it the date.
        /// </summary>
        /// <param name="name">The name of the goal.</param>
        /// <param name="amount">The target amount for the goal.</param>
        /// <returns>A new instance of a goal.</returns>
        IGoal CreateGoal(string name, string goalDescription, DateTime dueDate, string accDebitedID, decimal amount);
    }
}
