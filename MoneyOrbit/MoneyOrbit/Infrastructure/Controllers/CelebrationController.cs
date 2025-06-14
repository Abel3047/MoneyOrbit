using Microsoft.AspNetCore.Mvc;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Controllers
{
    public class CelebrationController : BaseController
    {
        private readonly ILogger<CelebrationController> _logger;

        public CelebrationController(ILogger<CelebrationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetTrophy")]
        public async Task<ActionResult> GetTrophy()
        {
            throw new NotImplementedException();
        }
    }
}