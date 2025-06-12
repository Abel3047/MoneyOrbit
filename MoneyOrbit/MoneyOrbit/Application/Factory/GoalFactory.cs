using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Factory
{
    public class GoalFactory : BaseFactory<Goal>, IGoalFactory<Goal>
    {
        public Goal CreateGoal(string goalName, string goalDescription, string accDebitedID, decimal amount, DateTime date)
        {
            return new Goal
            {
                ID = generators.GenerateKey(DateTime.Now),
                GoalName = goalName,
                GoalDescription = goalDescription,
                AccDebitedID = accDebitedID,
                Amount = amount,
                Date = date
            };
        }
    }
}
