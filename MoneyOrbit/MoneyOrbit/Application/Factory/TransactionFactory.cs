using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Factory
{
    public class TransactionFactory :BaseFactory<Transaction>, ITransactionFactory<Transaction>
    {
        public Transaction CreateTransaction(string? description, string accDebitedID, string? accCreditedID, decimal amount, DateTime? date)
        {
            return new Transaction
            {
                ID = generators.GenerateKey(DateTime.Now),
                Description = description,
                AccDebitedID = accDebitedID,
                AccCreditedID = accCreditedID,
                Amount = amount,
                Date = date
            };
        }
    }
}
