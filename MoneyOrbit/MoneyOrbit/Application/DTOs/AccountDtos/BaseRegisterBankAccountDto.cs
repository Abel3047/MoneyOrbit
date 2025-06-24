namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class BaseRegisterBankAccountDto
    {
        //Important so that you can register an account under a user
        required
        public string Token { get; set; }

        public string AccountName { get; set; }
        public string? description { get; set; }

        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankBranchCode { get; set; }
        public string BankBranchName { get; set; }
        public string? BankSwiftCode { get; set; }
    }
}
