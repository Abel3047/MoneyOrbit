using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            this._userService = userService;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser(UserCreationDto userCreationDto)
        {
            ResultObject resultObject = await _userService.RegisterUser(userCreationDto);

            if (resultObject.Error != null)
            {
                _logger.LogError("Error registering user: " + resultObject.Error);
                return BadRequest(resultObject.Error);
            }

            _logger.LogInformation("User registered successfully.");
            return Ok("User registered successfully.");
        }
    }
}
