namespace MoneyOrbit.Application.DTOs.MotivationDtos
{
    public class CreateTrophyDto
    {
        // the ID of the trophy can be randomly generated when stored in the database
        public string name { get; set; }
        public string dateEarned { get; set; }
    }
}