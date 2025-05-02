using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TechReserveSystem.Shared.Communication.Request.User;
using TechReserveSystem.Shared.Communication.Response.User;

namespace TechReserveSystem.API.Controllers.User
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices]
            [FromBody] RequestRegisterUserJson request
        )
    }
}