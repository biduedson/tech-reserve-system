using System.Reflection.Metadata;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;

namespace TechReserveSystem.Application.BusinessRules.Interfaces
{
    public interface IReservationBusinessRules
    {
        Task<User?> EnsureUserExists(Guid userId);
        Task<Equipment?> EnsureEquipmentExists(Guid equipmentId);
        Task<bool> IsEquipmentAvailableOnDate(Equipment equipment, DateTime reservationDate);
        Task<bool> VerifyUserPendingReservations(Guid userId);
        bool VerifyReservationNotInPast(DateTime reservationDate);
        bool VerifyReservationOneDayAhead(DateTime reservationDate);
        Task<bool> VerifyRejectedReservationForEquipmentOnDate(Guid userId, Guid equipmentId, DateTime date);
        Task<bool> VerifyExistingReservationForEquipmentOnDate(Guid userId, Guid equipmentId, DateTime date);
    }
}