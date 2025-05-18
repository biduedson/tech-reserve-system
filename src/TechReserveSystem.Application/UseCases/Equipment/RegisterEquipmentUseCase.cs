
using TechReserveSystem.Application.Interfaces.UseCases.Equipment;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response;
using TechReserveSystem.Shared.Communication.Response.Equipment;

namespace TechReserveSystem.Application.UseCases.Equipment
{
    public class RegisterEquipmentUseCase : IRegisterEquipmentUseCase
    {

        private readonly IEquipmentProcessingService _equipmentProcessingService;

        public RegisterEquipmentUseCase(IEquipmentProcessingService equipmentProcessingService)
        {
            _equipmentProcessingService = equipmentProcessingService;
        }

        public async Task<Response<ResponseRegisteredEquipmentJson>> Execute(RequestRegisterEquipmentJson request)
        {
            var response = await _equipmentProcessingService.Register(request);
            return response;
        }

    }
}