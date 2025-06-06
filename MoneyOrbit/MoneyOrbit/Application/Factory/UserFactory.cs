using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Factory
{
    public class UserFactory :BaseFactory<User>, IUserFactory<User>
    {
        public User CreateUser(string _userName, string _firstName, string _lastName, string _password)
        {
            Tuple<byte[], byte[]>  encryptedPasswordTuple = Generators.PasswordEncryptor(_password);
            User _user = new User() 
            {
                UserName=_userName, FirstName=_firstName, LastName=_lastName,
                ID = generators.GenerateKey(DateTime.Now), // Generate a new unique ID for the user
                PasswordHash = encryptedPasswordTuple.Item1, 
                PasswordSalt = encryptedPasswordTuple.Item2
            };
            return _user;

        }
    }
}
