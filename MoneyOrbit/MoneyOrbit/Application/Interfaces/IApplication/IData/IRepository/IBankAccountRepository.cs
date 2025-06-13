using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    /// <summary>
    /// Repository interface for managing goals. The understanding is that the opening path will already be set so as not to 
    /// cause confusion or breakage with multiple developers on this
    /// </summary>
    public interface IBankAccountRepository<TBankAccount> : IEntityRepository<TBankAccount> where TBankAccount : IBankAccount
    {
    }
}
