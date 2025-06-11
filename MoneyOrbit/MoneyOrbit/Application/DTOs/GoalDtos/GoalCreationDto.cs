namespace MoneyOrbit.Application.DTOs.GoalDtos
{
    public class GoalCreationDto
    {
        public DateTime? Date { get; set; }

        required
        public string GoalName
        { get; set; }
        required
        public string GoalDescription
        { get; set; }
        required
        public string AccDebitedID
        { get; set; }
        required
        public decimal Amount { get; set; }
    }
}