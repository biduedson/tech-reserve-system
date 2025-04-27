
namespace TechReserveSystem.Domain.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public int CategoryId { get; set; }
        public EquipmentCategory Category { get; set; }
    }
}