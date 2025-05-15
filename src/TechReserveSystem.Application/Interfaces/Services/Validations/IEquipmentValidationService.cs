using TechReserveSystem.Shared.Communication.Request.Equipment;

namespace TechReserveSystem.Application.Interfaces.Services.Validations
{
    public interface IEquipmentValidationService
    {
        void ValidateEquipmentRegisterRequest(RequestRegisterEquipmentJson request);

    }
}