using AutoMapper;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Application.Validators.EquipmentReservation;
using TechReserveSystem.Domain.Enuns;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Communication.constants;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Business;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.NotFound;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.UseCases.EquipmentReservation
{
    public class RegisterReservationUseCase : IRegisterReservationUseCase
    {
        private readonly IEquipmentReservationRepository _reservationRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterReservationUseCase(
            IEquipmentReservationRepository reservationRepository,
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _reservationRepository = reservationRepository;
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<EquipmentReservationResponse> Execute(EquipmentReservationRequest request)
        {
            var user = await _userRepository.GetById(request.UserId);
            if (user is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.USER_NOT_FOUND));

            var equipment = await _equipmentRepository.GetById(request.EquipmentId);
            if (equipment is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.EQUIPMENT_NOT_FOUND));

            Validate(request);

            var hasPendingReservations = await HasPendingReturn(request.UserId);
            Console.WriteLine(hasPendingReservations);
            if (hasPendingReservations)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.UNRETURNED_EQUIPMENT_RESTRICTION));
            }

            var isEquipmentReservationRejectedOnDate = await _reservationRepository.HasRejectedReservationOnDate(user.Id, equipment.Id, request.StartDate);

            var maxAllowedDate = IsReservationAllowed(request.StartDate);

            var isDateInPast = IsDateInPast(request.StartDate);
            if (isDateInPast)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_PAST_DATE));
            }

            if (maxAllowedDate)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_DATE_TOO_EARLY));
            }

            if (isEquipmentReservationRejectedOnDate)
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_ALREADY_REJECTED_ON_DATE));
            }

            if (await IsEquipmentAlreadyBookedByUser(request.UserId, request.EquipmentId, request.StartDate))
            {
                throw new BusinessException(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.EQUIPMENT_ALREADY_RESERVED_BY_USER));
            }
            var equipmentReservation = _mapper.Map<Domain.Entities.EquipmentReservation>(request);

            var isEquipmentUnavailable = await IsEquipmentUnavailable(request.StartDate, equipment);

            if (!isEquipmentUnavailable)
            {
                equipmentReservation.Status = ReservationStatus.Approved.ToString();
            }
            else
            {
                equipmentReservation.Status = ReservationStatus.Rejected.ToString();
            }
            equipmentReservation.ExpectedReturnDate = request.StartDate.Date;
            var reservation = await _reservationRepository.Add(equipmentReservation);
            await _unitOfWork.Commit();

            var result = new EquipmentReservationResponse
            {
                UserName = user.Name,
                EquipmentName = equipment.Name,
                ReservationStartDate = reservation.StartDate,
                ReservationEndDate = reservation.ExpectedReturnDate,
                Status = equipmentReservation.Status,
                Details = !isEquipmentUnavailable ?
                ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.RESERVATION_SUCCESS) :
                ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.EQUIPMENT_NOT_AVAILABLE)
            };

            return result;
        }

        private void Validate(EquipmentReservationRequest request)
        {
            var validator = new RegisterReservationValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private async Task<bool> IsEquipmentUnavailable(DateTime date, Domain.Entities.Equipment equipment)
        {
            var avaliableQuantity = await _reservationRepository.CountAvailableEquipmentOnDate(equipment, date);
            return avaliableQuantity == equipment.AvailableQuantity;
        }
        private bool IsReservationAllowed(DateTime startReservationDate)
        {
            var today = DateTime.Now.Date;
            return startReservationDate.Date == today;
        }

        private bool IsDateInPast(DateTime startReservationDate)
        {
            return startReservationDate.Date < DateTime.Now.Date;
        }
        private async Task<bool> HasPendingReturn(Guid userId)
        {
            var pendingReservations = await _reservationRepository.GetPendingReservationsByUser(userId);
            return pendingReservations.Any();
        }
        private async Task<bool> IsEquipmentAlreadyBookedByUser(Guid userId, Guid equipmentId, DateTime reservationDate)
        {
            var alreadyBookedByUser = await _reservationRepository.HasUserAlreadyReservedEquipment(userId, equipmentId, reservationDate);
            return alreadyBookedByUser;
        }

    }
}