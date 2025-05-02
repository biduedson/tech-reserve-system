using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Infrastructure.Data.Context;

namespace TechReserveSystem.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _DbContext;
        public UnitOfWork(AppDbContext context) => _DbContext = context;

        public async Task Commit() => await _DbContext.SaveChangesAsync();
    }
}