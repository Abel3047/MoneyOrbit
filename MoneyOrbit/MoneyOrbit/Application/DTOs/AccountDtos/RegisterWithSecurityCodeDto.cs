namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class RegisterWithSecurityCodeDto: BaseRegisterBankAccountDto
    {
        required
        public string SecuritCode { get; set; }
    }
}