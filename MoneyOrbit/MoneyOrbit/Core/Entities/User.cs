using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    /// <summary>   
    /// Represents a user in the system. This class implements the IEntity interface,
    /// indicating that it is an entity in the domain model.
    /// </summary>
    public class User: IUser
    {
        required
        public string ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //This will be phased out when @Terrence implements the authentication and authorization
        public string password { get; set; }

        //These I believe will be used by @Terrence when he implements the authentication and authorization
        public string ResetToken { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
