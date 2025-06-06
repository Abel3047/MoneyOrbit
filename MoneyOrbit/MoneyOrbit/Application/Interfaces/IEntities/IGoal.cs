namespace MoneyOrbit.Application.Interfaces.IEntities
{
    public interface IGoal : ITransaction
    {
        string GoalName { get; set; }
        string GoalDescription { get; set; }
    }
}
