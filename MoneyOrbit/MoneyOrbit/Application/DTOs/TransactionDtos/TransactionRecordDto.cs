namespace MoneyOrbit.Application.DTOs.TransactionDtos
{
    public class TransactionRecordDto
    {
        public string? Description { get; set; }
        public string? AccCreditedID { get; set; }

        public DateTime Date { get; set; }
        public string AccDebitedID { get; set; }
        public decimal Amount { get; set; }
    }
}