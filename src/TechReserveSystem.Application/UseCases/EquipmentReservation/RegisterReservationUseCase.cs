using AutoMapper;
using TechReserveSystem.Application.Interfaces.Services.Validations;
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Domain.Enuns;
using TechReserveSystem.Domain.Interfaces.Repositories;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;


namespace TechReserveSystem.Application.UseCases.EquipmentReservation
{
    public class RegisterReservationUseCase : IRegisterReservationUseCase
    {
        private readonly IEquipmentReservationRepository _reservationRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReservationValidationService _validationService;
        public RegisterReservationUseCase(
            IEquipmentReservationRepository reservationRepository,
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IReservationValidationService validationService
            )
        {
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _equipmentRepository = equipmentRepository ?? throw new ArgumentNullException(nameof(equipmentRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validationService = validationService ?? throw new ArgumentNullException(nameof(validationService));
        }
        public async Task<EquipmentReservationResponse> Execute(EquipmentReservationRequest request)
        {
            _validationService.ValidateReservationRequest(request);

            var (user, equipment) = await GetEntities(request.UserId, request.EquipmentId);

            await ValidateBusinessRules(request, user.Id, equipment.Id);

            var reservation = await CreateReservation(request, equipment);

            return CreateReservationResponse(user, equipment, reservation);

        }

        private async Task<(Domain.Entities.User user, Domain.Entities.Equipment equipment)> GetEntities(Guid userId, Guid equipmentId)
        {
            var user = await _userRepository.GetById(userId);
            var equipment = await _equipmentRepository.GetById(equipmentId);

            _validationService.ValidateEntitiesExist(user, equipment);

            return (user, equipment);
        }

        private async Task ValidateBusinessRules(EquipmentReservationRequest request, Guid userId, Guid equipmentId)
        {
            await _validationService.ValidateUserHasNoPendingReservations(userId);
            _validationService.ValidateReservationDate(request.StartDate);
            await _validationService.ValidateNoRejectedReservationsOnDate(userId, equipmentId, request.StartDate);
            await _validationService.ValidateNotAlreadyBookedByUser(userId, equipmentId, request.StartDate);
        }

        private async Task<Domain.Entities.EquipmentReservation> CreateReservation(
            EquipmentReservationRequest request,
            Domain.Entities.Equipment equipment)
        {
            var equipmentReservation = _mapper.Map<Domain.Entities.EquipmentReservation>(request);

            var isEquipmentUnavailable = await _reservationRepository.CountAvailableEquipmentOnDate(equipment, request.StartDate)
                                         >= equipment.AvailableQuantity;

            equipmentReservation.Status = isEquipmentUnavailable
                ? ReservationStatus.Rejected.ToString()
                : ReservationStatus.Approved.ToString();

            equipmentReservation.ExpectedReturnDate = request.StartDate.Date;

            var reservation = await _reservationRepository.Add(equipmentReservation);
            await _unitOfWork.Commit();

            return reservation;
        }

        private EquipmentReservationResponse CreateReservationResponse(
            Domain.Entities.User user,
            Domain.Entities.Equipment equipment,
            Domain.Entities.EquipmentReservation reservation)
        {
            var isApproved = reservation.Status == ReservationStatus.Approved.ToString();

            return new EquipmentReservationResponse
            {
                UserName = user.Name,
                EquipmentName = equipment.Name,
                ReservationStartDate = reservation.StartDate,
                ReservationEndDate = reservation.ExpectedReturnDate,
                Status = reservation.Status,
                Details = _validationService.GetReservationResponseDetails(isApproved)
            };
        }

    }
}