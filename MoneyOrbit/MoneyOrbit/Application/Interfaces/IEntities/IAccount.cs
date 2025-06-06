namespace MoneyOrbit.Application.Interfaces.IEntities
{
    public interface IAccount : IEntity
    {
        string? description { get; set; }
        bool isAsset { get; set; }
        bool isCaptial { get; set; }
        bool isLiability { get; set; }
    }
}
