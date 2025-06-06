namespace MoneyOrbit.Application.Interfaces.IEntities
{
    public interface ITransaction : IEntity
    {
        string ID { get; set; }
        DateTime Date { get; set; }
        string? Description { get; set; }
        string? AccDebitedID { get; set; }
        string? AccCreditedID { get; set; }
        decimal Amount { get; set; }
    }
}