using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository
{
    public interface IEquipmentRepository
    {
        Task<Equipment?> GetByIdAsync(Guid id);
        Task<Equipment?> GetByNameAsync(string name);
        Task<IEnumerable<Equipment>> GetAllAsync();
        Task<Equipment> AddAsync(Equipment equipment);
        Task UpdateAsync(Equipment equipment);
        Task DeleteAsync(Guid id);
    }
}