using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    public class Goal: Transaction,IGoal
    {
        required
        public string GoalName { get; set; }
        required
        public string GoalDescription { get; set; }
        required
        new public string AccDebitedID { get; set; }
        /// <summary>
        /// There may be multiple sources of income that will credit the Goal for it to be accomplished. The property reflects that
        /// </summary>
        public string[] AccountsCreditedID { get; set; } = Array.Empty<string>();

        private decimal amountAccomplished = 0;
        public decimal AmountAccomplished
        {
            get { return amountAccomplished; }
            set { amountAccomplished = value; }
        }

        /// <summary> Checks if the given transaction relates to this goal.
        /// A transaction relates to this goal if its debited account ID matches the goal's debited account ID
        /// and the transaction date is before the goal's date.
        /// </summary>
        /// <param name="transaction">The transaction to check.</param>
        /// <typeparam name="IGoal"> The type of the goal, which is expected to implement the IGoal interface.</typeparam>
        /// <returns>true if the transaction relates to this goal; otherwise, false.</returns>
        public bool RelatesToTransaction<IGoal>(ITransaction transaction)
        {
            return transaction.AccDebitedID == this.AccDebitedID &&
                   transaction.Date < this.Date;
        }

        /// <summary>
        /// Checks if all transactions in the given array relate to this goal.
        /// </summary>
        /// <typeparam name="IGoal"></typeparam>
        /// <param name="transactions"></param>
        /// <returns></returns>
        public bool RelatesToTransactions<IGoal>(ITransaction[] transactions)
        {
            // If any transaction does not relate to this goal, return false 
            // else it will continue to the next
            foreach (var transaction in transactions)
            {                
                if (!RelatesToTransaction<IGoal>(transaction)) return false;
                else continue;
            }
            return true;
        }

    }
}
