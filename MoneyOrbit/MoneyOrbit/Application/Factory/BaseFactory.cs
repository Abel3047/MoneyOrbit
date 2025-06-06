using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IApplication.IFactory;
using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Factory
{
    public class BaseFactory<TEntity>:IFactory<TEntity> where TEntity : IEntity
    {
        protected readonly Generators generators = new Generators();
    }
}
