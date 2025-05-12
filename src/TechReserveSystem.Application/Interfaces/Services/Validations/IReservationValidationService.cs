using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;

namespace TechReserveSystem.Application.Interfaces.Services.Validations
{
    public interface IReservationValidationService
    {
        void ValidateReservationRequest(EquipmentReservationRequest request);
        void ValidateEntitiesExist(User user, Equipment equipment);
        Task ValidateUserHasNoPendingReservations(Guid userId);
        void ValidateReservationDate(DateTime startDate);
        Task ValidateNoRejectedReservationsOnDate(Guid userId, Guid equipmentId, DateTime date);
        Task ValidateNotAlreadyBookedByUser(Guid userId, Guid equipmentId, DateTime date);
        string GetReservationResponseDetails(bool isApproved);
    }
}