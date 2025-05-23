using TechReserveSystem.Domain.Aggregates;
using TechReserveSystem.Domain.ValueObjects;

namespace TechReserveSystem.Domain.Interfaces.Services
{
    public interface IReservationService
    {
        Task<ReservationAggregate> Create(Guid userId, Guid equipmentId, int quantity, ReservationPeriod period);
    }
}