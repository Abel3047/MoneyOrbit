namespace MoneyOrbit.Application.DTOs.UserDtos
{
    public class UserUpdateDto
    {
        required //So you can identify the user in the database
        public string Token
        { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? password { get; set; }
    }
}