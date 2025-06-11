using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IGoalService
    {
        Task<ResultObject> AssignTransationToGoal(AssignTransationToGoalDto assignTransationToGoalDto);
        Task<ResultObject> CreateGoal(GoalCreationDto goalCreationDto);
        Task<ResultObject> UpdateGoal(GoalUpdateDto goalUpdateDto);
    }
}