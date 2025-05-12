using TechReserveSystem.Application.Interfaces.Services.Validations;
using TechReserveSystem.Application.Validators.EquipmentReservation;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Shared.Communication.constants;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.NotFound;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Validations
{
    public class ReservationValidationService : IReservationValidationService
    {
        private readonly IEquipmentReservationRepository _reservationRepository;

        public ReservationValidationService(IEquipmentReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        }

        public void ValidateReservationRequest(EquipmentReservationRequest request)
        {
            var validator = new RegisterReservationValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

        public void ValidateEntitiesExist(User user, Equipment equipment)
        {
            if (user is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.USER_NOT_FOUND));

            if (equipment is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.EQUIPMENT_NOT_FOUND));
        }

        public async Task ValidateUserHasNoPendingReservations(Guid userId)
        {
            var hasPendingReservations = await HasPendingReturn(userId);

            if (hasPendingReservations)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.UNRETURNED_EQUIPMENT_RESTRICTION));
            }
        }

        public void ValidateReservationDate(DateTime startDate)
        {
            if (IsDateInPast(startDate))
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_PAST_DATE));
            }

            if (IsReservationTooEarly(startDate))
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_DATE_TOO_EARLY));
            }
        }

        public async Task ValidateNoRejectedReservationsOnDate(Guid userId, Guid equipmentId, DateTime date)
        {
            var isRejectedOnDate = await _reservationRepository.HasRejectedReservationOnDate(userId, equipmentId, date);

            if (isRejectedOnDate)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_ALREADY_REJECTED_ON_DATE));
            }
        }

        public async Task ValidateNotAlreadyBookedByUser(Guid userId, Guid equipmentId, DateTime date)
        {
            var alreadyBookedByUser = await _reservationRepository.HasUserAlreadyReservedEquipment(userId, equipmentId, date);

            if (alreadyBookedByUser)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.EQUIPMENT_ALREADY_RESERVED_BY_USER));
            }
        }

        public string GetReservationResponseDetails(bool isApproved)
        {
            return isApproved
                ? ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.RESERVATION_SUCCESS)
                : ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.EQUIPMENT_NOT_AVAILABLE);
        }

        private async Task<bool> HasPendingReturn(Guid userId)
        {
            var pendingReservations = await _reservationRepository.GetPendingReservationsByUser(userId);
            return pendingReservations.Any();
        }

        private bool IsDateInPast(DateTime startReservationDate)
        {
            return startReservationDate.Date < DateTime.Now.Date;
        }

        private bool IsReservationTooEarly(DateTime startReservationDate)
        {
            // Regra de negócio: não é permitido fazer reserva para o mesmo dia
            var today = DateTime.Now.Date;
            return startReservationDate.Date == today;
        }
    }
}