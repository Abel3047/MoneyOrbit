using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    /// <summary>
    /// Repository interface for managing Users. The understanding is that the opening path will already be set so as not to 
    /// cause confusion or breakage with multiple developers on this
    /// </summary>
    public interface IUserRepository<TUser> : IEntityRepository<TUser> where TUser : IUser
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
