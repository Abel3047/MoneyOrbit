using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Application.Data.Repository
{
    public class TransactionRepository : EntityRepository, ITransactionRepository<ITransaction>
    {
        public TransactionRepository(IDataService dataService) : base(dataService, "Transactions") { }
        protected override string GetPropertyName() => "ID";

    }
}
