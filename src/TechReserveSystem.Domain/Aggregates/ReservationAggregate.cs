using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.ValueObjects;

namespace TechReserveSystem.Domain.Aggregates
{
    public class ReservationAggregate
    {
        public Guid Id { get; private set; }
        public User User { get; private set; }
        public Equipment Equipment { get; private set; }
        public int Quantity { get; private set; }
        public ReservationPeriod Period { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ReservationAggregate(Guid id, User user, Equipment equipment, int quantity, ReservationPeriod period, ReservationStatus status, DateTime createdAt)
        {
            Id = id;
            User = user;
            Equipment = equipment;
            Quantity = quantity;
            Period = period;
            Status = status;
            CreatedAt = createdAt;
        }
        public void Approve() => Status = ReservationStatus.Approved;
        public void Reject() => Status = ReservationStatus.Rejected;
        public void StartUsage() => Status = ReservationStatus.InProgress;
        public void Complete() => Status = ReservationStatus.Completed;
        public void Cancel() => Status = ReservationStatus.Cancelled;
    }
}