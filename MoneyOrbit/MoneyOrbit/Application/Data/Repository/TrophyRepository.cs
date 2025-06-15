using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class TrophyRepository : EntityRepository, ITrophyRepository<ITrophy>
    {
        public TrophyRepository(IDataService dataService) : base(dataService, "Trophy") { }

        protected override string GetPropertyName() => "";
    }
}