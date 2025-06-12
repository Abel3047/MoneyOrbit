using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Factory
{
    public class UserFactory :BaseFactory<User>, IUserFactory<User>
    {
        public User CreateUser(string _userName, string _firstName, string _lastName, string _password)
        {
            User _user = new User() 
            {
                UserName=_userName, FirstName=_firstName, LastName=_lastName,
                ID = generators.GenerateKey(DateTime.Now), // Generate a new unique ID for the user
                password = _password,
                //These should be uncommented when @Terrence implements the authentication and authorization
                //PasswordHash = null, 
                //PasswordSalt = null,
            };
            return _user;

        }
    }
}
