using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository
{
    public interface IEquipmentCategoryRepository
    {
        Task<EquipmentCategory?> GetById(Guid id);
        Task<EquipmentCategory?> GetByName(string name);
        Task<IEnumerable<EquipmentCategory>> GetAll();
        Task Add(EquipmentCategory category);
        void Update(EquipmentCategory category);
        void Delete(EquipmentCategory category);
        Task<bool> ExistEquipmentCategoryWithName(string name);
    }
}