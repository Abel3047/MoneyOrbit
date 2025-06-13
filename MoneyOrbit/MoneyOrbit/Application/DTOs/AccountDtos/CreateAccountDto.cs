namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class CreateAccountDto
    {
        public string Token { get; set; }

        public string AccountName { get; set; }
        public string? description { get; set; }

        public bool isAsset { get; set; }
        public bool isExpense { get; set; }
        public bool isCaptial { get; set; }
        public bool isLiability { get; set; }

        //BankAccount creation properties
        //Accounts are typically not bank accounts so the default is false
        public bool isBankAccount { get; set; } = false;

        public string? BankAccountName { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankBranchCode { get; set; }
        public string? BankBranchName { get; set; }
        public string? BankSwiftCode { get; set; }
    }
}