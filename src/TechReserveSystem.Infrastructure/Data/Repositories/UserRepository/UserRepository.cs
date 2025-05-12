using Microsoft.EntityFrameworkCore;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Infrastructure.Data.Context;

namespace TechReserveSystem.Infrastructure.Data.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<User?> GetById(Guid id)
        {
            var user = await _dbContext.Users
                 .Include(u => u.EquipmentReservations)
                 .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User?> GetByName(string name) => await _dbContext.Users.FindAsync(name);
        public async Task<User?> GetByEmail(string email) => await _dbContext.Users.FirstOrDefaultAsync(user => user.Email.Contains(email));
        public async Task<IEnumerable<User>> GetAll() => await _dbContext.Users.ToListAsync();
        public async Task Add(User user) => await _dbContext.Users.AddAsync(user);
        public void Update(User user) => _dbContext.Users.Update(user);
        public void Delete(User user) => _dbContext.Users.Remove(user);
        public async Task<bool> ExistActiveUserWithEmail(string email) =>
        await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);
    }
}