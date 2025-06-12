using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class UserRepository : EntityRepository, IUserRepository<IUser>
    {
        public UserRepository(IDataService dataService) : base(dataService, "Users") { }

        protected override string GetPropertyName() => "UserName";

        public override async Task<IUser> GetInstanceOfType<IUser>(string Token)
        {
            var userID = await Authenticator.VerifyOTP(Token,this);
            return await base.GetInstanceOfType<IUser>(userID);
        }

    }
}
