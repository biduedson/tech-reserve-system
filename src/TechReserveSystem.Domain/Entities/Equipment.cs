
namespace TechReserveSystem.Domain.Entities
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public Guid CategoryId { get; set; }

        public Equipment(string name, string description, int availableQuantity, Guid categoryId)
        {
            Name = name;
            Description = description;
            AvailableQuantity = availableQuantity;
            CategoryId = categoryId;
        }

    }
}