namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class UpdateAccountDto
    {
        public string  ID { get; set; }

        public string? description { get; set; }

        public bool isAsset { get; set; }
        public bool isExpense { get; set; }
        public bool isCaptial { get; set; }
        public bool isLiability { get; set; }
    }
}