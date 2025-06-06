using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IFactories
{
    public interface IFactory<TEntity> where TEntity : IEntity
    {
    }
}