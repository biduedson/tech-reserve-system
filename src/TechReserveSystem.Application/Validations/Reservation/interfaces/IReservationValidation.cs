using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;

namespace TechReserveSystem.Application.Validations.Reservation.interfaces
{
    public interface IReservationValidation
    {
        void Validation(EquipmentReservationRequest request);
    }
}