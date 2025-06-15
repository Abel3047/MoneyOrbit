using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class MotivationalStatementRepository : EntityRepository, IMotivationalStatementRepository<IMotivationStatement>
    {
        public MotivationalStatementRepository(IDataService dataService) : base(dataService, "MotivationalStatements") { }

        protected override string GetPropertyName() => "";
    }
}