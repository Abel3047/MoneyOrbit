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
        public string[] RelatedTransactionIDs { get; set; } = Array.Empty<string>();
        public decimal AmountAccomplished { get; set; }

        public void AddToAmountAccomplished(decimal amount) => AmountAccomplished += amount;
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

        new public string AccCreditedID { get; private set; }= "N/A"; // Goals are not credited, they are debited from related accounts.

    }
}
