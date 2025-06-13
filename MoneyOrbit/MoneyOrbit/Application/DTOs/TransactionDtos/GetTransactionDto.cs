namespace MoneyOrbit.Application.DTOs.TransactionDtos
{
    public class GetTransactionDto
    {
        required
        public string Token { get; set; }
        /// <summary>
        /// This is an optional parameter if you want to filter your results by specific accounts
        /// </summary>
        public string[]? AccIDs { get; set; }

        //These are also optional parameters to filter the results by date
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        //This to to filter users transactions that that which is backlogged
        public bool suspenseTransactions { get; set; } = false;
    }
}