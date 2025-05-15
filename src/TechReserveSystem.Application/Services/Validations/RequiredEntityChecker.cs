using TechReserveSystem.Application.Interfaces.Services.Validations;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.NotFound;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Services.Validations
{
    public class RequiredEntityChecker : IRequiredEntityChecker
    {
        private readonly IUserRepository _userRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentCategoryRepository _equipmentCategoryRepository;
        private readonly IEquipmentReservationRepository _equipmentReservationRepository;

        public RequiredEntityChecker(
            IUserRepository userRepository,
            IEquipmentRepository equipmentRepository,
            IEquipmentCategoryRepository equipmentCategoryRepository,
            IEquipmentReservationRepository equipmentReservationRepository
            )
        {
            _userRepository = userRepository;
            _equipmentRepository = equipmentRepository;
            _equipmentCategoryRepository = equipmentCategoryRepository;
            _equipmentReservationRepository = equipmentReservationRepository;
        }

        public async Task<User> User(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.USER_NOT_FOUND));
            return user;
        }
        public async Task<Equipment> Equipment(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetById(equipmentId);
            if (equipment is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.EQUIPMENT_NOT_FOUND));
            return equipment;
        }
        public async Task<EquipmentReservation> Reservation(Guid reservationId)
        {
            var reservation = await _equipmentReservationRepository.GetById(reservationId);
            if (reservation is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.RESERVATION_NOT_FOUND));
            return reservation;
        }
        public async Task<EquipmentCategory> Category(Guid categoryId)
        {
            var category = await _equipmentCategoryRepository.GetById(categoryId);
            if (category is null)
                throw new NotFoundExceptionError(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.RESERVATION_NOT_FOUND));
            return category;
        }
    }
}