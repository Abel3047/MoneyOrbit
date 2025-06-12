namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class AccountCreationDto
    {
        public string Token { get; set; }

        public string AccountName { get; set; }
        public string? description { get; set; }

        public bool isAsset { get; set; }
        public bool isExpense { get; set; }
        public bool isCaptial { get; set; }
        public bool isLiability { get; set; }
    }
}