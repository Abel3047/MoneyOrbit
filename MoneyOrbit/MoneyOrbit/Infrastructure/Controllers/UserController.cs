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

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser(UserCreationDto userCreationDto)
        {
            //Checks to see if the DTO is empty
            if(userCreationDto == null) 
                return BadRequest("User creation data is null.");

            //Checks if the important information is not null or empty
            if (String.IsNullOrEmpty(userCreationDto.password) ||
               String.IsNullOrEmpty(userCreationDto.UserName) ||
               String.IsNullOrEmpty(userCreationDto.FirstName) ||
               String.IsNullOrEmpty(userCreationDto.LastName) ||
               String.IsNullOrEmpty(userCreationDto.AccessLevel))
                return BadRequest("You are missing an important piece of information.Please provide a 'username'/password/firstname" +
                   "/lastname/AccessLevel.");

            //Checks if the email and phonenumber are in the correct format
            if(userCreationDto.Email!=null && !Validator.ValidateEmail(userCreationDto.Email))
                return BadRequest("This isn't the correct format for an email.");
            if (userCreationDto.PhoneNumber != null && !Validator.ValidatePhoneNumber(userCreationDto.PhoneNumber))
                return BadRequest("This isn't the correct format for a PhoneNumber.");

            // Check if the user already exists
            var existingUser = await _userService.GetUserByUserName(userCreationDto.UserName);
            if (existingUser != null)
                return BadRequest("User already exists");

            await _userService.CreateUser(userCreationDto);

            return Ok("User registered successfully.");
        }
    }
}
