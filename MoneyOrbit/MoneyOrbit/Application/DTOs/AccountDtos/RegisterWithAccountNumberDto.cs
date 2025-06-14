namespace MoneyOrbit.Application.DTOs.AccountDtos
{
    public class RegisterWithAccountNumberDto
    {
        public string Country { get; set; }
        public string IDType { get; set; }
        public string IDNumber { get; set; }
        public string AccountNumber { get; set; }

        // This is your "permission letter" variable.
        // It would likely be a token you received after the user granted consent.
        public string AuthorizationConsentToken { get; set; }
    }
}