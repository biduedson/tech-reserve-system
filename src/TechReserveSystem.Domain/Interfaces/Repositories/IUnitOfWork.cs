namespace TechReserveSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}