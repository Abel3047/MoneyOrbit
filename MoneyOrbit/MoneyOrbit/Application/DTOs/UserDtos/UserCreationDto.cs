namespace MoneyOrbit.Application.DTOs.UserDtos
{
    public class UserCreationDto
    {
        public string UserName { get; set; }
        public string password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
