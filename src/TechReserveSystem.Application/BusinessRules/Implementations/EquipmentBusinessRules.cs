using System.Text.RegularExpressions;
using TechReserveSystem.Application.BusinessRules.Interfaces;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;

namespace TechReserveSystem.Application.BusinessRules.Implementations
{
    public class EquipmentBusinessRules : IEquipmentBusinessRules
    {
        private readonly IEquipmentCategoryRepository _equipmentCategoryRepository;
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentBusinessRules
        (
            IEquipmentCategoryRepository equipmentCategoryRepository,
            IEquipmentRepository equipmentRepository
            )
        {
            _equipmentCategoryRepository = equipmentCategoryRepository;
            _equipmentRepository = equipmentRepository;
        }

        public bool IsAvailableQuantityValid(int quantity)
        {
            return quantity >= 1;
        }

        public async Task<EquipmentCategory?> EnsureCategoryExists(Guid categoyId)
        {
            return await _equipmentCategoryRepository.GetById(categoyId);
        }

        public async Task<bool> IsEquipmentNameAvailable(string equipmentName)
        {
            string normalizedName = Regex.Replace(equipmentName.Trim(), @"\s+", " ");
            return await _equipmentRepository.GetByName(normalizedName) == null;
        }
    }
}