using MoneyOrbit.Application.DTOs.AuthDtos;
using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Factory;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MoneyOrbit.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<IUser> _userRepository;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public UserService(IUserRepository<IUser> userRepository, IJwtTokenProvider jwtTokenProvider)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<AuthenticationResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            // If credentials are valid, generate the JWT
            var token = _jwtTokenProvider.Create(user);

            // Map to the response DTO
            return new AuthenticationResponseDto
            {
                Id = user.ID,
                UserName = user.UserName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<ResultObject> RegisterUser(UserCreationDto uCD)
        {
            //Checks to see if the DTO is empty
            if (uCD == null)
                return new ResultObject() { Error = "User creation data is null." };

            //Checks if the important information is not null or empty
            if (string.IsNullOrEmpty(uCD.password) ||
               string.IsNullOrEmpty(uCD.UserName) ||
               string.IsNullOrEmpty(uCD.FirstName) ||
               string.IsNullOrEmpty(uCD.LastName) ||
               string.IsNullOrEmpty(uCD.AccessLevel))
                return new ResultObject() { Error = "You are missing an important piece of information. Please provide a 'username'/password/firstname/lastname/AccessLevel." };

            //Checks if the email and phonenumber are in the correct format
            if (uCD.Email != null && !Validator.ValidateEmail(uCD.Email))
                return new ResultObject() { Error = "This isn't the correct format for an email." };
            if (uCD.PhoneNumber != null && !Validator.ValidatePhoneNumber(uCD.PhoneNumber))
                return new ResultObject() { Error = "This isn't the correct format for a PhoneNumber." };

            // Check if the user already exists
            if (await DoesUserNameExist(uCD.UserName))
                return new ResultObject() { Error = "User already exists" };

            // Create the password hash and salt from the user's password
            var (passwordHash, passwordSalt) = CreatePasswordHash(uCD.password);

            // Creates the user with information
            var user = new UserFactory()
                .CreateUser(uCD.UserName, uCD.FirstName, uCD.LastName, uCD.AccessLevel, uCD.Email, uCD.PhoneNumber);

            // IMPORTANT: Assign the generated hash and salt to the user object before saving
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Stores info in the database
            await _userRepository.UpdateData(user.ID, user);
            return new ResultObject() { Result = user.ID };
        }

        public async Task<ResultObject> UpdateUser(UserUpdateDto uUD)
        {
            User user = await _userRepository.GetInstanceOfType<User>(uUD.ID);
            if (!string.IsNullOrEmpty(uUD.FirstName)) user.FirstName = uUD.FirstName;
            if (!string.IsNullOrEmpty(uUD.LastName)) user.LastName = uUD.LastName;
            if (!string.IsNullOrEmpty(uUD.Email)) user.Email = uUD.Email;
            if (!string.IsNullOrEmpty(uUD.PhoneNumber)) user.PhoneNumber = uUD.PhoneNumber;
            await _userRepository.UpdateData(user.ID, user);
            return new ResultObject() { Result = "success" };
        }

        public async Task<ResultObject> UpdateUserPassword(string userID, string resetToken, string _newpassword)
        {
            if (string.IsNullOrEmpty(resetToken)) throw new NullReferenceException("You cannot have a null/empty resetToken");
            if (string.IsNullOrEmpty(_newpassword)) throw new NullReferenceException("You cannot have a null/empty _newpassword");

            //Code that @Terrence has to implement for resetToken authentication
            //throw new NotImplementedException("Terrence needs to implement resetToken authentication so that the rest of the method can" +
            //    "fire. He of course needs to test it as well");

            // This code will only execute after the above check is implemented and passes
            User user = await _userRepository.GetInstanceOfType<User>(userID);

            var (passwordHash, passwordSalt) = CreatePasswordHash(_newpassword);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

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
        private async Task<bool> DoesUserNameExist(string username) => await _userRepository.DoesPropertyExist(username);

        /// <summary>
        /// Creates a password hash and salt using HMACSHA512.
        /// </summary>
        /// <param name="password">The plain-text password.</param>
        /// <returns>A tuple containing the password hash and password salt.</returns>
        private (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (passwordHash, passwordSalt);
        }

        /// <summary>
        /// Verifies a password against a stored hash and salt.
        /// </summary>
        /// <param name="password">The plain-text password to verify.</param>
        /// <param name="passwordHash">The stored password hash.</param>
        /// <param name="passwordSalt">The stored password salt.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
        #endregion
    }
}