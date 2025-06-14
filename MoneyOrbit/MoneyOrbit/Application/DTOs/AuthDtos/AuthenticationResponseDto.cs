namespace MoneyOrbit.Application.DTOs.AuthDtos
{
    public class AuthenticationResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; } // The JWT
    }
}
