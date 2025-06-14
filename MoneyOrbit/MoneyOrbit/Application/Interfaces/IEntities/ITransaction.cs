namespace MoneyOrbit.Application.Interfaces.IEntities
{
    public interface ITransaction : IEntity
    {
        string ID { get; set; }

        DateTime? Date { get; set; }
        string? Description { get; set; }
        string? AccCreditedID { get; set; }

        /// <summary>
        /// This is an important string because this is what all transactions will be indexed on. We don't need to know who each
        /// transaction was done by, just where the amount went to. And Each user holds information about the accounts they are 
        /// associated with
        /// </summary>
        string AccDebitedID { get; set; }
        decimal Amount { get; set; }
    }
}