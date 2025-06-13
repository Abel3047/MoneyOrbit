namespace MoneyOrbit.Application.Interfaces.IEntities
{
    public interface IGoal : ITransaction
    {
        string GoalName { get; set; }
        string GoalDescription { get; set; }
        string[] RelatedTransactionIDs { get; set; }
        decimal AmountAccomplished { get; }

        /// <summary> Checks if the given transaction relates to this goal.
        /// A transaction relates to this goal if its debited account ID matches the goal's debited account ID
        /// and the transaction date is before the goal's date.
        /// </summary>
        /// <param name="transaction">The transaction to check.</param>
        /// <typeparam name="IGoal"> The type of the goal, which is expected to implement the IGoal interface.</typeparam>
        /// <returns>true if the transaction relates to this goal; otherwise, false.</returns>
        bool RelatesToTransaction<IGoal>(ITransaction transaction);
        /// <summary>
        /// This adds the amount to the AmountAccomplished property of the goal. This should be the only way to do so as the 
        /// accomplished amount should only be modified by the transactions that relate to the goal, so its made as a 'readonly' property.
        /// </summary>
        /// <param name="amount"></param>
        void AddToAmountAccomplished(decimal amount);
    }
}
