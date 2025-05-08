using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository
{
    public interface IEquipmentReservationRepository
    {
        Task<EquipmentReservation?> GetById(Guid id);
        Task<IEnumerable<EquipmentReservation?>> GetByUserId(Guid userId);
        Task<IEnumerable<EquipmentReservation?>> GetByEquipmetId(Guid equipmentId);
        Task<int> CountAvailableEquipmentOnDate(Equipment equipment, DateTime date);
        Task<IEnumerable<EquipmentReservation>> GetAll();
        Task<EquipmentReservation> Add(EquipmentReservation equipmentReservation);
        Task<EquipmentReservation> Update(EquipmentReservation equipmentReservation);
        void Delete(EquipmentReservation equipmentReservation);
    }
}