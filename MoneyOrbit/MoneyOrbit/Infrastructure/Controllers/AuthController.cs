using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.AuthDtos;
using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Controllers
{
    [Route("MoneyOrbit/[controller]")]
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _userService.GeneratePasswordResetTokenAsync(forgotPasswordDto.Email);

            if (result.Error != null)
            {
                // This could happen if the email format is invalid, for example.
                return BadRequest(new { Message = result.Error });
            }

            // For security, always return a generic success message.
            // This prevents attackers from discovering which email addresses are registered in your system.
            return Ok(new { Message = "If an account with this email exists, a password reset link has been sent." });
        }
   
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            // Note: In a real-world scenario, you might get the UserId from the JWT token
            // if the user is already logged in, but for a "forgot password" flow,
            // the user is not logged in, so passing it in the DTO is appropriate.

            var result = await _userService.UpdateUserPassword(
                resetPasswordDto.UserId,
                resetPasswordDto.Token,
                resetPasswordDto.NewPassword
            );

            if (result.Error != null)
            {
                return BadRequest(new { Message = result.Error });
            }

            return Ok(new { Message = "Your password has been successfully updated." });
        }
    }
}