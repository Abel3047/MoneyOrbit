using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class AccountRepository : EntityRepository, IAccountRepository<IAccount>
    {
        public AccountRepository(IDataService dataService) : base(dataService, "Account") { }

        protected override string GetPropertyName() => "Account";
    }
}
