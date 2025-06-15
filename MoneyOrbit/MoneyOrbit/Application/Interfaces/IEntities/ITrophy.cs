namespace MoneyOrbit.Application.Interfaces.IEntities
{
    public interface ITrophy : IEntity
    {
        string name { get; set; }
        string dateEarned { get; set; }
    }
}