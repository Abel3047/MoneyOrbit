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
        public async Task<ActionResult> CreateAccount(CreateAccountDto accountCreationDto)
        {
            ResultObject resultObject = await _accountService.CreateAccount(accountCreationDto);

            if (resultObject.Error != null)
            {
                _logger.LogError("Error creating an account: " + resultObject.Error);
                return BadRequest(resultObject.Error);
            }


            _logger.LogInformation("Creating an account");
            return Ok("Account registered successfully.");
        }
        [HttpPost("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount(DeleteAccountDto deleteAccountDto)
        {
            ResultObject resultObject = await _accountService.DeleteAccount(deleteAccountDto);

            if (resultObject.Error != null)
            {
                _logger.LogError("Error deleting the account: " + resultObject.Error);
                return BadRequest(resultObject.Error);
            }

            _logger.LogInformation("Account deleted successfully.");
            return Ok("Account deleted successfully.");
        }
        [HttpPost("RegisterWithAccountNumber")]
        public async Task<ActionResult> RegisterWithAccountNumber(RegisterWithAccountNumberDto registerWithAccountNumberDto)
        {
            ResultObject resultObject = await _accountService.RegisterWithAccountNumber(registerWithAccountNumberDto);

            if (resultObject.Error != null)
            {
                _logger.LogError("Error linking the bank account: " + resultObject.Error);
                return BadRequest(resultObject.Error);
            }

            _logger.LogInformation("Link request successfully sent to the bank.");
            return Ok("Request to your bank as been sent. Wait 3 business working days to confirm linkage.");
        }
        [HttpPost("RegisterWithSecurityCode")]
        public async Task<ActionResult> RegisterWithAccountNumber(RegisterWithSecurityCodeDto registerWithSecurityCodeDto)
        {
            ResultObject resultObject = await _accountService.RegisterWithSecurityCode(registerWithSecurityCodeDto);

            if (resultObject.Error != null)
                return BadRequest(resultObject.Error);

            return Ok("Request to your bank as been sent. Wait 3 business working days to confirm linkage.");
        }
    }
}
