using MoneyOrbit.Application.DTOs.MotivationDtos;
using MoneyOrbit.Application.Helpers;

public interface ICelebrationService
{
    /// <summary>
    /// This method returns a trophy from a given trophy ID.
    /// </summary>
    /// <param name="tdto"></param>
    Task<ResultObject> GetTrophy(GetTrophyDto tdto);
    /// <summary>
    /// This method creates a trophy in the database.
    /// </summary>
    /// <param name="tdto"></param>
    /// <returns></returns>
    Task<ResultObject> SetTrophy(CreateTrophyDto tdto);
    /// <summary>
    /// This method gets the motivational statement from the database using the given ID
    /// </summary>
    /// <param name="mdto"></param>
    /// <returns></returns>
    Task<ResultObject> GetMotivationalStatement(GetMotivationStatementDto mdto);
    /// <summary>
    /// This method creates motivational statement and stores it in the database
    /// </summary>
    /// <param name="mdto"></param>
    /// <returns></returns>
    Task<ResultObject> SetMotivationalStatement(CreateMotivationalStatementDto mdto);

}