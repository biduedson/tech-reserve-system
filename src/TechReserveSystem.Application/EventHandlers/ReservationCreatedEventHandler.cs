using MediatR;
using TechReserveSystem.Application.Events.Reservation;
using TechReserveSystem.Domain.Exceptions;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.ValueObjects;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.EventHandlers
{
    public class ReservationCreatedEventHandler : INotificationHandler<MediatRReservationCreatedEvent>
    {
        private readonly IUserAggregateRepository _userAggregateRepository;

        public ReservationCreatedEventHandler(IUserAggregateRepository userAggregateRepository)
        {
            _userAggregateRepository = userAggregateRepository;
        }

        public async Task Handle(MediatRReservationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userReservations = await _userAggregateRepository.GetUserReservationsAsync(notification.UserId);

            if (userReservations.Any(r => r.Status == ReservationStatus.Pending))
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.UNRETURNED_EQUIPMENT_RESTRICTION));

            if (userReservations.Any(r => r.Status == ReservationStatus.Rejected && r.Period.StartDate == notification.StartDate))
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_ALREADY_REJECTED_ON_DATE));
        }
    }
}