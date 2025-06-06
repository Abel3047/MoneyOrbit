using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    /// <summary>Represents a financial account in the system.
    /// This class implements the IEntity interface, indicating that it is an entity in the domain model.
    /// </summary>
    public class Account: IAccount
    {
        public string ID { get; set; }
        public string? description { get; set; }
    }
}
