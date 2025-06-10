using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.DTOs.AccountDtos;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult> CreateAccount(AccountCreationDto accountCreationDto)
        {
            ResultObject resultObject = await _accountService.CreateAccount(accountCreationDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Account registered successfully.");
        }
    }
}
