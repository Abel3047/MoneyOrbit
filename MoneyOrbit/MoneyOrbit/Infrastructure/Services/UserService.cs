using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository<IUser> _userRepository;

        public UserService(IUserRepository<IUser> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<string> CreateUser(UserCreationDto uCD)
        {
            var user= new UserFactory().CreateUser(uCD.UserName, uCD.FirstName, uCD.LastName, uCD.password);
            await _userRepository.UpdateData(user.ID, user);
            return user.ID;
        }
        public async Task UpdateUser(UserUpdateDto uUD)
        {
            User user = await _userRepository.GetInstanceOfType<User>(uUD.ID);
            if (!String.IsNullOrEmpty(uUD.FirstName)) user.FirstName = uUD.FirstName;
            if (!String.IsNullOrEmpty(uUD.LastName)) user.LastName = uUD.LastName;
            if (!String.IsNullOrEmpty(uUD.Email)) user.Email = uUD.Email;
            if (!String.IsNullOrEmpty(uUD.PhoneNumber)) user.PhoneNumber = uUD.PhoneNumber;
            await _userRepository.UpdateData(user.ID, user);
        }
        public async Task UpdateUserPassword(string userID, string resetToken, string _newpassword)
        {          
            if (String.IsNullOrEmpty(resetToken)) throw new NullReferenceException("You cannot have a null/empty resetToken");
            if (String.IsNullOrEmpty(_newpassword)) throw new NullReferenceException("You cannot have a null/empty _newpassword");

            //Code that @Terrence has to implement for resetToken authentication
            throw new NotImplementedException("Terrence needs to implement resetToken authentication so that the rest of the method can" +
                "fire. He of course needs to test it as well");

            User user = await _userRepository.GetInstanceOfType<User>(userID);            

            var encryptedPasswordTuple = new Generators().PasswordEncryptor(_newpassword);
            user.PasswordHash = encryptedPasswordTuple.Item1;
            user.PasswordSalt = encryptedPasswordTuple.Item2;

            await _userRepository.UpdateData(user.ID, user);
        }
        public async Task<User> GetUserById(string userId) => await _userRepository.GetInstanceOfType<User>(userId);
        public async Task DeleteUser(string userId) => await _userRepository.DeleteData(userId);
    }
}
