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

            var equipment = await _equipmentRepository.GetById(request.EquipmentId);
            if (equipment is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.EQUIPMENT_NOT_FOUND));

            var user = await _userRepository.GetById(request.UserId);
            if (user is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.USER_NOT_FOUND));

            Validate(request);

            var equipmentReservation = _mapper.Map<Domain.Entities.EquipmentReservation>(request);

            var isEquipmentAvailability = await IsEquipmentUnavailable(request.StartDate, equipment);

            if (!isEquipmentAvailability)
            {
                equipmentReservation.Status = ReservationStatus.Rejected.ToString();
            }

            var reservation = await _reservationRepository.Add(equipmentReservation);
            var result = new EquipmentReservationResponse
            {
                UserName = user.Name,
                EquipmentName = equipment.Name,
                ReservationStartDate = reservation.StartDate,
                ReservationEndDate = reservation.ExpectedReturnDate,
                Status = reservation.Status,
                Details = isEquipmentAvailability ?
                ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.RESERVATION_SUCCESS) :
                ResourceAppMessages.GetCommunicationMessage(ReservationDetailsMessages.EQUIPMENT_NOT_AVAILABLE)
            };
            await _unitOfWork.Commit();

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

    }
}