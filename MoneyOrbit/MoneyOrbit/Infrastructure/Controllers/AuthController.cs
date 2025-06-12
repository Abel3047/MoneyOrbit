using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.AuthDtos;
using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserCreationDto userCreationDto)
        {
            ResultObject resultObject = await _userService.RegisterUser(userCreationDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok(new { UserId = resultObject.Result });
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var response = await _userService.Login(loginDto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}