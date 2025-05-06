namespace TechReserveSystem.Domain.Entities
{
    public class EquipmentCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
    }
}