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

        public ReservationAggregate(Guid id, Guid userId, Guid equipmentId, int quantity, ReservationPeriod period, ReservationStatus status, DateTime createdAt)
        {
            if (quantity <= 0) throw new BusinessRuleException(ReservationErrorMessages.AVAILABLE_QUANTITY_EQUIPMENT_INVALID);
            Id = id;
            UserId = userId;
            EquipmentId = equipmentId;
            Quantity = quantity;
            Period = period;
            Status = status;
            CreatedAt = createdAt;
        }

        private void ValidateBusinessRules()
        {

        }
        public void Approve() => Status = ReservationStatus.Approved;
        public void Reject() => Status = ReservationStatus.Rejected;
        public void StartUsage() => Status = ReservationStatus.InProgress;
        public void Complete() => Status = ReservationStatus.Completed;
        public void Cancel() => Status = ReservationStatus.Cancelled;
    }
}