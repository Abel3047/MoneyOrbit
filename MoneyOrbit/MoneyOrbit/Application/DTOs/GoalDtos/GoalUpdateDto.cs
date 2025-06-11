namespace MoneyOrbit.Application.DTOs.GoalDtos
{
    public class GoalUpdateDto
    {
        public DateTime? Date { get; set; }
        public string? GoalName
        { get; set; }
        public string? GoalDescription
        { get; set; }
        public decimal Amount { get; set; }
    }
}