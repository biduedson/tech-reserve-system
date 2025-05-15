using TechReserveSystem.Shared.Communication.Request.Equipment;

namespace TechReserveSystem.Application.Validations.Equipment.Interfaces
{
    public interface IEquipmentValidation
    {
        void Validation(RequestRegisterEquipmentJson request);
    }
}