using MediatR;
using TechReserveSystem.Domain.Events.Reservation;

namespace TechReserveSystem.Application.Events.Reservation
{
    public class MediatRReservationCreatedEvent : ReservationCreatedEvent, INotification
    {
        public MediatRReservationCreatedEvent(Guid reservationId, Guid userId, Guid equipmentId, DateTime startDate)
            : base(reservationId, userId, equipmentId, startDate) { }
    }
}