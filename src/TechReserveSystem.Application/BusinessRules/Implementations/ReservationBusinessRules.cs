using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentReservationRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.UserRepository;

namespace TechReserveSystem.Application.BusinessRules.Implementations
{
    public class ReservationBusinessRules : IReservationBusinessRules
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEquipmentReservationRepository _reservationRepository;

        public ReservationBusinessRules(
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository,
            IEquipmentReservationRepository reservationRepository
            )
        {
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<Equipment?> EnsureEquipmentExists(Guid equipmentId)
        {
            var equipment = await _equipmentRepository.GetById(equipmentId);
            return equipment;
        }

        public async Task<User?> EnsureUserExists(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            return user;
        }

        public async Task<bool> IsEquipmentAvailableOnDate(Equipment equipment, DateTime reservationDate)
        {
            return await _reservationRepository.CountAvailableEquipmentOnDate(equipment, reservationDate) < equipment.AvailableQuantity;
        }

        public async Task<bool> VerifyUserPendingReservations(Guid userId)
        {
            var pendingReservations = await _reservationRepository.GetPendingReservationsByUser(userId);
            return pendingReservations.Any();
        }

        public bool VerifyReservationNotInPast(DateTime reservationDate)
        {
            return reservationDate.Date < DateTime.Now.Date;
        }

        public bool VerifyReservationOneDayAhead(DateTime reservationDate)
        {
            var today = DateTime.Now.Date;
            return reservationDate.Date == today;
        }

        public async Task<bool> VerifyRejectedReservationForEquipmentOnDate(Guid userId, Guid equipmentId, DateTime date)
        {
            return await _reservationRepository.HasRejectedReservationOnDate(userId, equipmentId, date);
        }

        public async Task<bool> VerifyExistingReservationForEquipmentOnDate(Guid userId, Guid equipmentId, DateTime date)
        {
            return await _reservationRepository.HasUserAlreadyReservedEquipment(userId, equipmentId, date);
        }

    }
}