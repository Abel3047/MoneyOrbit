using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    /// <summary>
    /// Repository interface for managing Users. The understanding is that the opening path will already be set so as not to 
    /// cause confusion or breakage with multiple developers on this
    /// </summary>
    public interface IUserRepository<TUser> : IEntityRepository<TUser> where TUser : IUser
    {
        /// <summary>
        /// This takes in an <paramref name="token"/> that will be inputed from the client so that a user can have access to its
        /// user information, which most critically involves the <see cref="IAccount"/>s involved with the user
        /// </summary>
        /// <typeparam name="IUser"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        new Task<IUser> GetInstanceOfType<IUser>(string token);
    }
}