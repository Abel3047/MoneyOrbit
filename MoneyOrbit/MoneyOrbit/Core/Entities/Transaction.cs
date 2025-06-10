using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    public class Transaction : ITransaction, IEquatable<ITransaction>
    {
        public string ID { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        
        public string AccDebitedID { get; set; }
        public string? AccCreditedID { get; set; }
        public decimal Amount { get; set; }

        /// <summary>
        /// <para>Compares this transaction with another transaction for equality.
        /// Two transactions are considered equal if they have the same date, debited 
        /// account ID, credited account ID, and amount.</para>       
        /// The same is said when comparing a Goal with a Goal. But when a Goal is compared with a Transaction,
        /// this is the same as comparing a Buget to a transaction that has the same properties.
        /// </summary>
        /// <param name="transaction">The transaction to compare with.</param>
        /// <returns>true if the transactions are equal; otherwise, false.</returns>
        bool IEquatable<ITransaction>.Equals(ITransaction? transaction)
        {
            // If the current instance is null but the transaction is not null, return false.
            if (this== null && NullGuard.IsNotNull(transaction))return false;
            // If the current instance and the transaction are both Goals, compare only the relevant properties.
            if (this is IGoal goal && transaction is IGoal goalTransaction)
            {
                return Date == goalTransaction.Date &&
                       AccDebitedID == goalTransaction.AccDebitedID &&
                       Amount == goalTransaction.Amount;
            }
            // You can't compare a Goal with a Transaction, so return false.
            if (this is IGoal goal1 && transaction is Transaction transactionObj) return false;
            // If the current instance and the transaction are both Transactions, compare all properties.
            return Date == transaction.Date &&
                   AccDebitedID == transaction.AccDebitedID &&
                   AccCreditedID == transaction.AccCreditedID &&
                   Amount == transaction.Amount;
        }        
    }
}
