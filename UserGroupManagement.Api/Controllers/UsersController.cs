using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserGroupManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { Message = "Hello, Swagger works!" });
    }
}
