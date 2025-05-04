using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechReserveSystem.Application.UseCases.User.Register;
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
            [FromServices] IRegisterUserUseCase useCase,
            [FromBody] RequestRegisterUserJson request
        )
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}