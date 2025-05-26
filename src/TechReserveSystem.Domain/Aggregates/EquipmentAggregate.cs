using TechReserveSystem.Domain.Constants;
using TechReserveSystem.Domain.Exceptions;

namespace TechReserveSystem.Domain.Aggregates
{
    public class EquipmentAggregate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int TotalStock { get; private set; }

        public EquipmentAggregate(Guid id, string name, string description, int totalStock)
        {
            if (totalStock <= 0)
                throw new DomainException(EquipmentErrorMessages.INVALIDE_EQUIPMENT_STOCK);

            Id = id;
            Name = name;
            Description = description;
            TotalStock = totalStock;
        }
        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(EquipmentErrorMessages.EQUIPMENT_STOCK_INCREASE_INVALID);

            TotalStock += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException(EquipmentErrorMessages.EQUIPMENT_STOCK_DECREASE_INVALID);

            if (TotalStock < quantity)
                throw new DomainException(EquipmentErrorMessages.EQUIPMENT_STOCK_DECREASE_INSUFFICIENT);
            TotalStock -= quantity;
        }
    }
}