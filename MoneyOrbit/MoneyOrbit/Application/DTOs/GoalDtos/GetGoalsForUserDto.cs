namespace MoneyOrbit.Application.DTOs.GoalDtos
{
    public class GetGoalsForUserDto
    {
        required
        public string userID { get; set; }
        //These are also optional parameters to filter the results by date
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}