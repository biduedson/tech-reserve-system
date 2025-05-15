using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Application.Interfaces.Services.Validations
{
    public interface IRequiredEntityChecker
    {
        Task<User> User(Guid userId);
        Task<Equipment> Equipment(Guid equipmentId);
        Task<EquipmentReservation> Reservation(Guid reservationId);
        Task<EquipmentCategory> Category(Guid categoryId);
    }
}