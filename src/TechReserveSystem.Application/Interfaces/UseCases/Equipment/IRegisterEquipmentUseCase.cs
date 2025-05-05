using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Response.Equipment;

namespace TechReserveSystem.Application.Interfaces.UseCases.Equipment
{
    public interface IRegisterEquipmentUseCase
    {
        public Task<ResponseRegisteredEquipmentJson> Execute(RequestRegisterEquipmentJson request);
    }
}