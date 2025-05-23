using TechReserveSystem.Domain.Aggregates;
using TechReserveSystem.Domain.Exceptions;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Domain.Interfaces.Services;
using TechReserveSystem.Domain.ValueObjects;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentReservationRepository _equipmentReservationRepository;

        public ReservationService(IUserRepository userRepository, IEquipmentRepository equipmentRepository, IEquipmentReservationRepository equipmentReservationRepository)
        {
            _userRepository = userRepository;
            _equipmentRepository = equipmentRepository;
            _equipmentReservationRepository = equipmentReservationRepository;
        }
        public async Task<ReservationAggregate> Create(Guid userId, Guid equipmentId, int quantity, ReservationPeriod period)
        {
            await EnsureRoles(userId, equipmentId, quantity, period);

            var reservationAggregate = new ReservationAggregate(userId, equipmentId, quantity, period);

            await DetermineReservationOutcome(reservationAggregate);

            return reservationAggregate;

        }

        private async Task EnsureRoles(Guid userId, Guid equipmentId, int quantity, ReservationPeriod period)
        {
            bool userExisting = await _equipmentReservationRepository.UserExisting(userId);
            if (!userExisting)
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.USER_NOT_FOUND));

            bool equipmentExisting = await _equipmentReservationRepository.EquipmentExisting(equipmentId);
            if (!equipmentExisting)
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.EQUIPMENT_NOT_FOUND));

            bool hasPendingReservations = await _equipmentReservationRepository.GetUserOverdueReservationsCount(userId) > 0;
            if (hasPendingReservations)
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.UNRETURNED_EQUIPMENT_RESTRICTION));

            bool reservationAlreadyRejected = await _equipmentReservationRepository.HasRejectedReservationOnDate(userId, equipmentId, period.StartDate);
            if (reservationAlreadyRejected)
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_ALREADY_REJECTED_ON_DATE));
        }

        private async Task DetermineReservationOutcome(ReservationAggregate reservation)
        {
            bool isEquipmentAvailableOnDate = await _equipmentReservationRepository.CountAvailableEquipmentOnDate(reservation.EquipmentId, reservation.Period.StartDate) >= reservation.Quantity;
            if (!isEquipmentAvailableOnDate)
            {
                reservation.Reject();
            }
            else
            {
                reservation.Approve();
            }
        }
    }
}