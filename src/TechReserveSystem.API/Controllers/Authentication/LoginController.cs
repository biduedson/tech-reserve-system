using Microsoft.AspNetCore.Mvc;
using TechReserveSystem.Application.Interfaces.UseCases.Login;
using TechReserveSystem.Shared.Communication.Request.Authentication;
using TechReserveSystem.Shared.Communication.Response.Authentication;

namespace TechReserveSystem.API.Controllers.Authentication
{
    public class Logincontroller : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        public Logincontroller(ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }
        [HttpPost("api/auth/login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]

        public async Task<IActionResult> Login([FromBody] UserLoginRequest request, [FromServices] ILoginUseCase useCase)
        {
            var userLogged = await useCase.Execute(request);
            return Ok(userLogged);
        }
    }
}