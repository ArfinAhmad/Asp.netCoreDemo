using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [Authorize]
        public IActionResult Get()
        {
            return Ok(new string[] {"Customer one", "Customer two","customer three"});
        }
    }
}
