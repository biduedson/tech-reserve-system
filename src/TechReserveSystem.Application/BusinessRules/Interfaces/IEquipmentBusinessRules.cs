using TechReserveSystem.Domain.Entities;

namespace TechReserveSystem.Application.BusinessRules.Interfaces
{
    public interface IEquipmentBusinessRules
    {
        bool IsAvailableQuantityValid(int quantity);
        Task<EquipmentCategory?> EnsureCategoryExists(Guid categoyId);
        Task<bool> IsEquipmentNameAvailable(string equipmentName);
    }
}