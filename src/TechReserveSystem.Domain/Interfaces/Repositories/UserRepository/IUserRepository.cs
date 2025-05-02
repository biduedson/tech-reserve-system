using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Domain.Interfaces.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(string email);
        Task<User?> GetByName(string name);
        Task<bool> ExistActiveUserWithEmail(string email);
        Task<IEnumerable<User>> GetAll();
        Task Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}