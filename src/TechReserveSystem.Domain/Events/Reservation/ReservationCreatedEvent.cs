namespace TechReserveSystem.Domain.Events.Reservation
{
    public class ReservationCreatedEvent : BaseEvent
    {
        public Guid ReservationId { get; }
        public Guid UserId { get; }
        public Guid EquipmentId { get; }
        public DateTime StartDate { get; }

        public ReservationCreatedEvent(Guid reservationId, Guid userId, Guid equipmentId, DateTime startDate)
        {
            ReservationId = reservationId;
            UserId = userId;
            EquipmentId = equipmentId;
            StartDate = startDate;
        }
    }
}