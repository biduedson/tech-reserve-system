using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository
{
    public interface IEquipmentRepository
    {
        Task<Equipment?> GetById(Guid id);
        Task<Equipment?> GetByName(string name);
        Task<IEnumerable<Equipment>> GetAll();
        Task Add(Equipment equipment);
        void Update(Equipment equipment);
        void Delete(Equipment equipment);
        Task<bool> ExistEquipmentWithName(string name);
    }
}