using TechReserveSystem.Domain.Constants;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Exceptions;
using TechReserveSystem.Domain.ValueObjects;

namespace TechReserveSystem.Domain.Aggregates
{
    public class ReservationAggregate
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid EquipmentId { get; private set; }
        public int Quantity { get; private set; }
        public ReservationPeriod Period { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ReservationAggregate(Guid userId, Guid equipmentId, int quantity, ReservationPeriod period)
        {
            if (quantity <= 0) throw new DomainException(ReservationErrorMessages.AVAILABLE_QUANTITY_EQUIPMENT_INVALID);
            Id = new Guid();
            UserId = userId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            Period = period;
            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        private void ValidateBusinessRules()
        {

        }
        private ReservationAggregate(Guid id, Guid userId, Guid equipmentId, int quantity, ReservationPeriod period, ReservationStatus status, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            Period = period;
            Status = status;
            CreatedAt = createdAt;
        }
        public static ReservationAggregate FromDatabase(Guid id, Guid userId, Guid equipmentId, int quantity, ReservationPeriod period, ReservationStatus status, DateTime createdAt)
        {
            return new ReservationAggregate(id, userId, equipmentId, quantity, period, status, createdAt);
        }

        public void Approve() => Status = ReservationStatus.Approved;
        public void Reject() => Status = ReservationStatus.Rejected;
        public void StartUsage() => Status = ReservationStatus.InProgress;
        public void Complete() => Status = ReservationStatus.Completed;
        public void Cancel() => Status = ReservationStatus.Cancelled;
    }
}