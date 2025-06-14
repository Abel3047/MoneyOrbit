namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class LinkBankAccountDto
    {
        required
        public string IDNumber { get; set; }
        required
        public string IDType { get; set; }
        required
        public string? AccountNumber { get; set; }
        required
        public bool isSecondOption
        { get; set; } = false;
        public string? Country { get; set; }
        public string? SecurityCode { get; set; }

    }
}