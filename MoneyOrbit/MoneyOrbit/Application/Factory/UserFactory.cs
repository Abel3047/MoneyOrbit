using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Core.Entities;
using static MoneyOrbit.Core.Models.Enums;

namespace MoneyOrbit.Application.Factory
{
    public class UserFactory :BaseFactory<User>, IUserFactory<User>
    {
        public User CreateUser(string _userName, string _firstName, string _lastName, string _password)
        {
            Tuple<byte[], byte[]>  encryptedPasswordTuple = generators.PasswordEncryptor(_password);
            User _user = new User() 
            {
                UserName=_userName, FirstName=_firstName, LastName=_lastName,
                ID = generators.GenerateKey(DateTime.Now), // Generate a new unique ID for the user
                PasswordHash = encryptedPasswordTuple.Item1, 
                PasswordSalt = encryptedPasswordTuple.Item2
            };
            return _user;

        }
        public User CreateUser(string _userName, string _firstName, string _lastName, string _password,string userpermission)
        {
            string permission = "";

            case (UserPermissions.Customer.ToString()):
                permission = UserPermissions.Customer.ToString();
            case (UserPermissions.Administrative.ToString()):
                permission = UserPermissions.Customer.ToString();
            case (UserPermissions.Development.ToString()):
                permission = UserPermissions.Customer.ToString();
            case _
                throw new NullReferenceException("You need to provide a valid userpermission");

            Tuple<byte[], byte[]> encryptedPasswordTuple = generators.PasswordEncryptor(_password);
            User _user = new User()
            {
                UserName = _userName,
                FirstName = _firstName,
                LastName = _lastName,
                ID = generators.GenerateKey(DateTime.Now), // Generate a new unique ID for the user
                PasswordHash = encryptedPasswordTuple.Item1,
                PasswordSalt = encryptedPasswordTuple.Item2,
                AccessLevel= permission
            };
            return _user;

        }
    }
}
