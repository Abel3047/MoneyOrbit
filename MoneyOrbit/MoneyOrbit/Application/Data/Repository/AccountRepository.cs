using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class AccountRepository : EntityRepository, IAccountRepository<IAccount>
    {
        public AccountRepository(IDataService dataService) : base(dataService, "Accounts") { }

        /// <summary>
        /// We have proper access the the Accounts ID because that will be how its stored. But we want to know if an account belongs
        /// to a user or not. And this is where this methods use comes alive. 
        /// </summary>
        /// <returns></returns>
        protected override string GetPropertyName() => "AccountName";
        
    }
}