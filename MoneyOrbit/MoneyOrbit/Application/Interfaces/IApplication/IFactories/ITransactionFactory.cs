using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    internal interface ITransactionFactory<TTransaction>:IFactory<TTransaction> where TTransaction : ITransaction
    {
        /// <summary>
        /// Creates a new transaction with the specified parameters. It should provide the date for the transaction.
        /// <para>Note: It should generate the id from a combination of first the accDebitID and the accCreditedID</para>
        /// </summary>
        /// <param name="accDebitedID">The account ID that is debited in this transaction.</param>
        /// <param name="accCreditedID">The account ID that is credited in this transaction.</param>
        /// <param name="amount">The amount of money involved in the transaction.</param>
        /// <returns>A new instance of a transaction.</returns>
        ITransaction CreateTransaction(string? description, string accDebitedID, string accCreditedID, decimal amount);
    }
}
