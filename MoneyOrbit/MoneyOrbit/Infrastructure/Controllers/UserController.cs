using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.UserDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;
using System.Security.Claims;

namespace MoneyOrbit.Infrastructure.Controllers
{
    [Route("MoneyOrbit/[controller]")]
    [Authorize] // <-- THIS SECURES THE ENTIRE CONTROLLER
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            // The user's ID is retrieved from the validated token (the 'sub' claim).
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            // You might want to map this to a DTO to avoid exposing the full entity.
            return Ok(user);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null || userUpdateDto.ID != userId)
            {
                // Prevent a user from trying to update another user's profile
                return Forbid("You can only update your own profile.");
            }

            var result = await _userService.UpdateUser(userUpdateDto);
            if (result.Error != null)
            {
                return BadRequest(result.Error);
            }

            return NoContent(); // Success, no content to return
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrative")] // Requires the user's token to have an 'Admin' role claim
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
