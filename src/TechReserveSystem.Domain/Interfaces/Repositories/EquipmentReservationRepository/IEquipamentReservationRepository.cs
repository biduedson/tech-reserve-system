using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository
{
    public interface IEquipmentReservationRepository
    {
        Task<EquipmentReservation?> GetById(Guid id);
        Task<IEnumerable<EquipmentReservation>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<EquipmentReservation>> GetByEquipmetIdAsync(Guid equipmentId);
        Task<IEnumerable<EquipmentReservation>> GetAllAsync();
        Task<EquipmentReservation> AddAsync(EquipmentReservation equipmentReservation);
        Task<EquipmentReservation> UpdateAsync(EquipmentReservation equipmentReservation);
        Task DeleteAsync(Guid id);
    }
}