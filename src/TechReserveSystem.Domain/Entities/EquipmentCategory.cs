namespace TechReserveSystem.Domain.Entities
{
    public class EquipmentCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();
    }
}