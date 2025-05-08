using Microsoft.AspNetCore.Mvc;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;

namespace TechReserveSystem.API.Controllers.EquipmentReservation
{
    [Route("api/equipment-reservation/register")]
    [ApiController]
    public class EquipmentReservationController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(EquipmentReservationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(
                    [FromServices] IRegisterReservationUseCase _usecase,
                    [FromBody] EquipmentReservationRequest request
                )
        {
            var result = await _usecase.Execute(request);
            return Ok(result);
        }
    }
}