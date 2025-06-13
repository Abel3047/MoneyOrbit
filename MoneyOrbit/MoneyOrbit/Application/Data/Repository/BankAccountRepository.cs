using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class BankAccountRepository : AccountRepository, IBankAccountRepository<IBankAccount>
    {
        public BankAccountRepository(IDataService dataService) : base(dataService) { }

        public override async Task<IBankAccount> GetInstanceOfType<IBankAccount>(string Token)
        {
            var userID = await Authenticator.VerifyOTP(Token, this);
            return await base.GetInstanceOfType<IBankAccount>(userID);
        }
    }
}