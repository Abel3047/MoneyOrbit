using Microsoft.AspNetCore.Mvc;

namespace MoneyOrbit.Infrastructure.Controllers
{    
    [ApiController]
    [Route("MoneyOrbit/[controller]")]
    public class BaseController : ControllerBase
    {
        public BaseController(){ }
    }
}
