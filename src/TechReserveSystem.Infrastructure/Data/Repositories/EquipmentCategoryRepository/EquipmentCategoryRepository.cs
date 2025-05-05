using Microsoft.EntityFrameworkCore;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Infrastructure.Data.Context;

namespace TechReserveSystem.Infrastructure.Data.Repositories.EquipmentCategoryRepository
{
    public class EquipmentCategoryRepository : IEquipmentCategoryRepository
    {
        private readonly AppDbContext _dbContent;
        public EquipmentCategoryRepository(AppDbContext dbContent) => _dbContent = dbContent;

        public async Task<EquipmentCategory?> GetById(Guid id) => await _dbContent.EquipmentCategories.FindAsync(id);
        public async Task<EquipmentCategory?> GetByName(string name) => await _dbContent.EquipmentCategories.FirstOrDefaultAsync(equiupment => equiupment.Name.Contains(name));
        public async Task<IEnumerable<EquipmentCategory>> GetAll() => await _dbContent.EquipmentCategories.ToListAsync();
        public async Task Add(EquipmentCategory category) => await _dbContent.EquipmentCategories.AddAsync(category);
        public void Update(EquipmentCategory category) => _dbContent.EquipmentCategories.Update(category);
        public void Delete(EquipmentCategory category) => _dbContent.EquipmentCategories.Remove(category);
        public async Task<bool> ExistEquipmentCategoryWithName(string name) => await _dbContent.EquipmentCategories.AnyAsync(category => category.Name.Equals(name));
    }
}