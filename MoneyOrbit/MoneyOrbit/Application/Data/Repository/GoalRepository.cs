using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class GoalRepository : EntityRepository, IGoalRepository<IGoal>
    {
        public GoalRepository(IDataService dataService) : base(dataService, "Goals") { }

        protected override string GetPropertyName() => "ID";
    }
}
