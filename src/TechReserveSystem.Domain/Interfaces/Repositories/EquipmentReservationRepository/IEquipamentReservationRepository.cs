using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository
{
    public interface IEquipmentReservationRepository
    {
        Task<bool> UserExisting(Guid userId);
        Task<bool> EquipmentExisting(Guid equipmentId);
        Task<int> CountAvailableEquipmentOnDate(Guid equipmentId, DateTime reservationDate);
        Task<int> GetUserOverdueReservationsCount(Guid userId);
        Task<EquipmentReservation?> GetById(Guid id);
        Task<IEnumerable<EquipmentReservation?>> GetByUserId(Guid userId);
        Task<IEnumerable<EquipmentReservation?>> GetByEquipmetId(Guid equipmentId);
        Task<bool> HasRejectedReservationOnDate(Guid userId, Guid equipmentId, DateTime date);
        Task<IEnumerable<EquipmentReservation>> GetPendingReservationsByUser(Guid userId);
        Task<bool> HasUserAlreadyReservedEquipment(Guid userId, Guid equipmentId, DateTime reservationDate);
        Task<int> CountAvailableEquipmentOnDate(Equipment equipment, DateTime date);
        Task<IEnumerable<EquipmentReservation>> GetAll();
        Task<EquipmentReservation> Add(EquipmentReservation equipmentReservation);
        Task<EquipmentReservation> Update(EquipmentReservation equipmentReservation);
        void Delete(EquipmentReservation equipmentReservation);
    }
}