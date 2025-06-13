namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class LinkBankAccountDto
    {
        required
        public string BankAccountName { get; set; }
        required
        public string BankAccountNumber { get; set; }
        required
        public string BankBranchCode { get; set; }
        required
        public string BankBranchName { get; set; }
        public string? BankSwiftCode { get; set; }
    }
}