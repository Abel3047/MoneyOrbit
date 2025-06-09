using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class UserRepository : EntityRepository, IUserRepository<IUser>
    {
        public UserRepository(IDataService dataService) : base(dataService, "Users") { }

        protected override string GetPropertyName() => "UserName";

    }
}
