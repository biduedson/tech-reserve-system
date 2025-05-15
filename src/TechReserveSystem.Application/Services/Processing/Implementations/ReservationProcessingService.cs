using AutoMapper;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Application.Validations.Reservation.interfaces;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Enuns;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Shared.Communication.constants;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Processing.Implementations
{
    public class ReservationProcessingService : IReservationProcessingService
    {
        private readonly IEquipmentReservationRepository _equipmentReservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReservationBusinessRules _reservationBusinessRules;
        private readonly IReservationValidation _reservationValidation;

        public ReservationProcessingService(
            IEquipmentReservationRepository equipmentReservationRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IReservationBusinessRules reservationBusinessRules,
            IReservationValidation reservationValidation
            )
        {
            _equipmentReservationRepository = equipmentReservationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reservationBusinessRules = reservationBusinessRules;
            _reservationValidation = reservationValidation;
        }

        public async Task<EquipmentReservationResponse> Register(EquipmentReservationRequest request)
        {
            var result = new EquipmentReservationResponse();
            EnsureValidationRules(request);
            var reservation = _mapper.Map<EquipmentReservation>(request);
            await EnsureBusinessRules(request, result, reservation);
            await _equipmentReservationRepository.Add(reservation);
            await _unitOfWork.Commit();

            return result;

        }

        private void EnsureValidationRules(EquipmentReservationRequest request)
        {
            _reservationValidation.Validation(request);
        }
        private async Task EnsureBusinessRules(EquipmentReservationRequest request, EquipmentReservationResponse result, EquipmentReservation reservation)
        {
            var user = await _reservationBusinessRules.EnsureUserExists(request.UserId)
            ?? throw new BusinessException(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.USER_NOT_FOUND));

            var equipment = await _reservationBusinessRules.EnsureEquipmentExists(request.EquipmentId)
            ?? throw new BusinessException(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.EQUIPMENT_NOT_FOUND));
            DateTime reservationDate = request.StartDate;

            bool hasPendingReservations = await _reservationBusinessRules.VerifyUserPendingReservations(request.UserId);
            if (hasPendingReservations)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.UNRETURNED_EQUIPMENT_RESTRICTION));

            bool isReservationDateInPast = _reservationBusinessRules.VerifyReservationNotInPast(reservationDate);
            if (isReservationDateInPast)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_PAST_DATE));

            bool isReservationMinimumDaysMet = _reservationBusinessRules.VerifyReservationOneDayAhead(reservationDate);
            if (isReservationMinimumDaysMet)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_DATE_TOO_EARLY));

            bool isReservationRejectedForEquipmentOnDate = await _reservationBusinessRules.VerifyRejectedReservationForEquipmentOnDate(request.UserId, request.EquipmentId, reservationDate);
            if (isReservationRejectedForEquipmentOnDate)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_ALREADY_REJECTED_ON_DATE));

            bool isUserReservedForEquipmentOnDate = await _reservationBusinessRules.VerifyExistingReservationForEquipmentOnDate(request.UserId, request.EquipmentId, reservationDate);
            if (isUserReservedForEquipmentOnDate)
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.EQUIPMENT_ALREADY_RESERVED_BY_USER));

            bool isEquipmentAvailableOnDate = await _reservationBusinessRules.IsEquipmentAvailableOnDate(equipment, reservationDate);

            if (isEquipmentAvailableOnDate)
            {
                result.Status = ReservationStatus.Approved.ToString();
                result.Details = ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.RESERVATION_SUCCESS);
                reservation.Status = ReservationStatus.Approved.ToString();

            }
            else
            {
                result.Status = ReservationStatus.Rejected.ToString();
                result.Details = ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.EQUIPMENT_NOT_AVAILABLE);
                reservation.Status = ReservationStatus.Rejected.ToString();
            }

            result.UserName = user.Name;
            result.EquipmentName = equipment.Name;

            result.ReservationEndDate = reservationDate;
            result.ReservationStartDate = reservationDate;
            reservation.StartDate = reservationDate;
            reservation.ExpectedReturnDate = reservationDate;

        }

    }
}