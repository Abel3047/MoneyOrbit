using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    /// <summary>
    /// Repository interface for managing goals. The understanding is that the opening path will already be set so as not to 
    /// cause confusion or breakage with multiple developers on this
    /// </summary>
    public interface IAccountRepository<TAccount> : IEntityRepository<TAccount> where TAccount : IAccount
    {
    }
}
