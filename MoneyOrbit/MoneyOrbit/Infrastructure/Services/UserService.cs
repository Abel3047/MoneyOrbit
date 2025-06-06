using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Factory;
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
        public async Task UpdateUserPassword(string userID, string token)
        {
            User user = await _userRepository.GetInstanceOfType<User>(userID);

            if (String.IsNullOrEmpty(token)) throw new NullReferenceException("You cannot have a null/empty token");

            //Code that @Terrence has to implement for passwordHash and salt generation
            throw new NotImplementedException("Terrence needs to implement the password and salt generation pattern and put the" +
                "call to an instance of it here");

            await _userRepository.UpdateData(user.ID, user);
        }
        public async Task<User> GetUserById(string userId) => await _userRepository.GetInstanceOfType<User>(userId);
        public async Task DeleteUser(string userId) => await _userRepository.DeleteData(userId);
    }
}
