namespace TechReserveSystem.Domain.Events
{
    public abstract class BaseEvent
    {
        public DateTime OccurredOn { get; private set; }
        public Guid EventId { get; private set; }

        protected BaseEvent()
        {
            OccurredOn = DateTime.UtcNow;
            EventId = Guid.NewGuid();
        }
    }
}