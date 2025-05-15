using Microsoft.EntityFrameworkCore;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Infrastructure.Data.Context;

namespace TechReserveSystem.Infrastructure.Data.Repositories.EquipmentRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _dbContext;

        public EquipmentRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<Equipment?> GetById(Guid id) => await _dbContext.Equipments.FindAsync(id);
        public async Task<Equipment?> GetByName(string name) => await _dbContext.Equipments.FirstOrDefaultAsync(Equipment => Equipment.Name.Equals(name));
        public async Task<IEnumerable<Equipment>> GetAll() => await _dbContext.Equipments.ToListAsync();
        public async Task Add(Equipment equipment) => await _dbContext.Equipments.AddAsync(equipment);
        public void Update(Equipment equipment) => _dbContext.Equipments.Update(equipment);
        public void Delete(Equipment equipment) => _dbContext.Equipments.Remove(equipment);
        public async Task<bool> ExistEquipmentWithName(string name) =>
        await _dbContext.Equipments.AnyAsync(equipment => equipment.Name.Equals(name));
    }
}