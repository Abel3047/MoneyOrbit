using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    internal interface IUserFactory<TUser>:IFactory<TUser> where TUser : IUser
    {
        /// <summary>
        /// Creates a new User with the specified parameters. It should generate the id for the user.
        /// </summary>
        /// <param name="name">The name of the goal.</param>
        /// <param name="amount">The target amount for the goal.</param>
        /// <returns>A new instance of a User.</returns>
        User CreateUser(string _userName, string _firstName, string _lastName, string _password, string _accesslevel,
                               string _email = "", string _phonenumber = "");
    }
}
