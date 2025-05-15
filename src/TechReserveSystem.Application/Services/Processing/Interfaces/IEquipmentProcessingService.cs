using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response.Equipment;

namespace TechReserveSystem.Application.Services.Processing.Interfaces
{
    public interface IEquipmentProcessingService
    {
        Task<ResponseRegisteredEquipmentJson> RegisterEquipment(RequestRegisterEquipmentJson request);
    }
}