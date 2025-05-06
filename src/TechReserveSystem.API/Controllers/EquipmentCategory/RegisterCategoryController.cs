using Microsoft.AspNetCore.Mvc;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentCategory;
using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Communication.Response.EquipmentCategory;

namespace TechReserveSystem.API.Controllers.EquipmentCategory
{
  [Route("api/equipment-category/register")]
  [ApiController]
  public class RegisterCategoryController : ControllerBase
  {
    [HttpPost]
    [ProducesResponseType(typeof(RegisterEquipmentCategoryResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
      [FromServices] IRegisterEquipmentCategoryUseCase useCases,
      [FromBody] EquipmentCategoryRegisterRequest request
    )
    {
      var result = await useCases.Execute(request);
      return Created(string.Empty, result);
    }
  }
}