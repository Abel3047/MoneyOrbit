using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    /// <summary>
    /// This class represents the trophy that are stored in the system. This class implements the IEntity interface,
    /// indicating that it is an entity in the domain model.
    /// </summary>
    public class Trophy : ITrophy
    {
        public string ID { get; set; }
        public string name { get; set; }
        public string dateEarned { get; set; }
    }
}