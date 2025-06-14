using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Core.Entities
{
    /// <summary>
    /// This class represents the motivational statement that will be shown to the 
    /// user.
    /// </summary>
    public class MotivationStatement : IMotivationStatement
    {
        public string ID { get; set; }
        public string statement { get; set; }
    }
}