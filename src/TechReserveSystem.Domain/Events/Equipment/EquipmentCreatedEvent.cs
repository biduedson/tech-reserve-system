namespace TechReserveSystem.Domain.Events.Equipment
{
    public class EquipmentCreatedEvent : BaseEvent
    {
        public string Name { get; private set; }
        public Guid CategoryId { get; private set; }

        public EquipmentCreatedEvent(string name, Guid categoryId)
        {
            Name = name;
            CategoryId = categoryId;
        }
    }
}