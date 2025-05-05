using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response.Equipment;

namespace TechReserveSystem.API.Controllers.Equipment
{
    //[Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredEquipmentJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterEquipmentUseCase useCase,
            [FromBody] RequestRegisterEquipmentJson request
        )
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}