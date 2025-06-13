using System.Security;
using MoneyOrbit.Application.Interfaces.IApplication.IFactories;
using MoneyOrbit.Core.Entities;
using static MoneyOrbit.Core.Models.Enums;

namespace MoneyOrbit.Application.Factory
{
    public class UserFactory :BaseFactory<User>, IUserFactory<User>
    {
        public User CreateUser(string _userName, string _firstName, string _lastName, string _password, string _accesslevel,
                               string _email= "", string _phonenumber = "")
        {
            //This takes in the _accesslevel given by the method and tries to convert it to one of the enum list. 
            //If it doesn't show up as one of them it will spit out that error, 
            //else it will set the variable permission and assign it to the User below
            //if (!Enum.TryParse(_accesslevel, out UserPermissions permission))
            //{
            //    throw new ArgumentException("Invalid user permission");
            //}

            Tuple<byte[], byte[]> encryptedPasswordTuple = generators.PasswordEncryptor(_password);
            User _user = new User()
            {
                UserName = _userName,
                FirstName = _firstName,
                LastName = _lastName,
                Email= _email,
                PhoneNumber= _phonenumber,
                ID = generators.GenerateKey(DateTime.Now),
                PasswordHash = encryptedPasswordTuple.Item1,
                PasswordSalt = encryptedPasswordTuple.Item2,
                //AccessLevel = permission.ToString(),
                AccessLevel = _accesslevel,
                AccountIDs = []
            };
            return _user;
        }
    }
}
