namespace MoneyOrbit.Application.DTOs.GoalDtos
{
    public class AssignTransationToGoalDto
    {
        required
        public string TransactionID { get; set; }
        public string GoalID { get; set; }
    }
}