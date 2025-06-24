namespace MoneyOrbit.Application.DTOs.UserDtos
{
    public class LoginResponseDto
    {
        public string token { get; set; } = null!;
        public string accessLevel { get; set; } = null!;
    }
}