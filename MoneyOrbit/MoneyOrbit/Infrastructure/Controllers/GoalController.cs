using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.GoalDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Controllers
{
    public class GoalController : BaseController
    {
        private readonly ILogger<GoalController> _logger;
        private readonly IGoalService _goalService;

        public GoalController(ILogger<GoalController> logger, IGoalService goalService)
        {
            _logger = logger;
            _goalService = goalService;
        }

        [HttpPost("CreateGoal")]
        public async Task<ActionResult> CreateGoal(GoalCreationDto goalCreationDto)
        {
            ResultObject resultObject = await _goalService.CreateGoal(goalCreationDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Goal created successfully.");
        }
        [HttpPost("UpdateGoal")]
        public async Task<ActionResult> UpdateGoal(GoalUpdateDto goalUpdateDto)
        {
            ResultObject resultObject = await _goalService.UpdateGoal(goalUpdateDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Goal updated successfully.");
        }
        [HttpPost("AssignTransationToGoal")]
        public async Task<ActionResult> AssignTransationToGoal(AssignTransationToGoalDto assignTransationToGoalDto)
        {
            ResultObject resultObject = await _goalService.AssignTransationToGoal(assignTransationToGoalDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Goal updated successfully.");
        }
    }
}
