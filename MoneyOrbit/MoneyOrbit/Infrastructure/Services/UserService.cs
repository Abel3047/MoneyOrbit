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

        public async Task<ResultObject> RegisterUser(UserCreationDto uCD)
        {
            //Checks to see if the DTO is empty
            if (uCD == null)
                return new ResultObject() { Error = "User creation data is null." };

            //Checks if the important information is not null or empty
            if (String.IsNullOrEmpty(uCD.password) ||
               String.IsNullOrEmpty(uCD.UserName) ||
               String.IsNullOrEmpty(uCD.FirstName) ||
               String.IsNullOrEmpty(uCD.LastName) ||
               String.IsNullOrEmpty(uCD.AccessLevel))
                return new ResultObject() { Error = "You are missing an important piece of information.Please provide a" +
                    " 'username'/password/firstname/lastname/AccessLevel."};

            //Checks if the email and phonenumber are in the correct format
            if (uCD.Email != null && !Validator.ValidateEmail(uCD.Email))
                return new ResultObject(){ Error = "This isn't the correct format for an email." };
            if (uCD.PhoneNumber != null && !Validator.ValidatePhoneNumber(uCD.PhoneNumber))
                return new ResultObject() { Error = "This isn't the correct format for a PhoneNumber." };

            // Check if the user already exists
            if (await DoesUserNameExist(uCD.UserName))
            return new ResultObject() { Error = "User already exists" };

            //Creates the user with information
            var user = new UserFactory()
                .CreateUser(uCD.UserName, uCD.FirstName, uCD.LastName, uCD.password, uCD.AccessLevel, uCD.Email, uCD.PhoneNumber);

            //Stores info in the database
            await _userRepository.UpdateData(user.ID, user);
            return  new ResultObject() { Result= user.ID};
        }
        public async Task<ResultObject> UpdateUser(UserUpdateDto uUD)
        {
            User user = await _userRepository.GetInstanceOfType<User>(uUD.ID);
            if (!String.IsNullOrEmpty(uUD.FirstName)) user.FirstName = uUD.FirstName;
            if (!String.IsNullOrEmpty(uUD.LastName)) user.LastName = uUD.LastName;
            if (!String.IsNullOrEmpty(uUD.Email)) user.Email = uUD.Email;
            if (!String.IsNullOrEmpty(uUD.PhoneNumber)) user.PhoneNumber = uUD.PhoneNumber;
            await _userRepository.UpdateData(user.ID, user);
            return new ResultObject() { Result = "success" };
        }
        public async Task<ResultObject> UpdateUserPassword(string userID, string resetToken, string _newpassword)
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
            return new ResultObject() { Result = "success" };
        }
        public async Task<User> GetUserById(string userId) => await _userRepository.GetInstanceOfType<User>(userId);
        public async Task<ResultObject> DeleteUser(string userId)
        {
            await _userRepository.DeleteData(userId);
            return new ResultObject() { Result = "success" };
        }
        #region Support methods
        /// <summary>
            /// Checks if the username already exists in the database by checking if the username is within a certain path configuration
            /// </summary>
            /// <param name="username"></param>
            /// <returns> False if it does not exist in the database</returns>
        private async Task<bool> DoesUserNameExist(string username)=> await _userRepository.DoesPropertyExist(username);
        #endregion
    }
}
