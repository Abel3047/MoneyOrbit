using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    /// <summary>
    /// Repository interface for managing trophies.
    /// </summary>
    public interface ITrophyRepository<TTrophy> : IEntityRepository<TTrophy> where TTrophy : ITrophy
    {
    }
}