using TechReserveSystem.Domain.Aggregates;

namespace TechReserveSystem.Domain.Interfaces.Repositories
{
    public interface IUserAggregateRepository
    {
        Task<UserAggregate?> GetByIdAsync(Guid id);
        Task<IReadOnlyCollection<ReservationAggregate>> GetUserReservationsAsync(Guid userId);

    }
}