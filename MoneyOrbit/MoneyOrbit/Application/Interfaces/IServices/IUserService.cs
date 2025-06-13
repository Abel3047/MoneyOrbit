using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.DTOs.AuthDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// This method allows a user to login to the system
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        Task<AuthenticationResponseDto> Login(LoginDto loginDto);

        /// <summary>
        /// This method creates a user using <see cref="UserFactory.CreateUser(string, string, string, string)"/> 
        /// stores the user in the database partition <see cref="IUserRepository{TUser}"/> and outputs the User ID
        /// </summary>
        /// <param name="uCD"></param>
        /// <returns></returns>
        Task<ResultObject> RegisterUser(UserCreationDto uCD);


        /// <summary>
        /// Checks if the properties in <see cref="UserUpdateDto"/> are not null. If not null it will update the user it gets
        /// from the database by the <paramref name="uUD.ID"/>
        /// </summary>
        /// <param name="uUD"></param>
        /// <returns></returns>
        Task<ResultObject> UpdateUser(UserUpdateDto uUD);


        /// <summary>
        /// Takes in the parameters to make a new passwordHash and passwordSalt for user based on the new password
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="resetToken"></param>
        /// <param name="_newpassword"></param>
        /// <returns></returns>
        Task<ResultObject> UpdateUserPassword(string userID, string resetToken, string _newpassword);

        /// <summary>
        /// Generates a password reset token for a user based on their email,
        /// saves it to the database, and returns the token.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <returns>A ResultObject containing the reset token on success.</returns>
        Task<ResultObject> GeneratePasswordResetTokenAsync(string email);

        /// <summary>
        /// Gets the user from the database by its ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<User> GetUserById(string userId);


        /// <summary>
        /// Deletes the user and its data by its path (which is its ID)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResultObject> DeleteUser(string userId);

    }
}