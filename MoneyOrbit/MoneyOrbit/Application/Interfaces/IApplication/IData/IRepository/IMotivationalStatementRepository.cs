using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    /// <summary>
    /// Repository interface for managing motivational statements.
    /// </summary>
    public interface IMotivationalStatementRepository<TMotivationalStatement> : IEntityRepository<TMotivationalStatement> where TMotivationalStatement : IMotivationStatement
    {
    }
}