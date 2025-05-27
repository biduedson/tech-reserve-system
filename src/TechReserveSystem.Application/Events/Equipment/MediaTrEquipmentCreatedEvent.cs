using MediatR;
using TechReserveSystem.Domain.Events.Equipment;

namespace TechReserveSystem.Application.Events.Equipment
{
    public class MediaTrEquipmentCreatedEvent : EquipmentCreatedEvent, INotification
    {
        public MediaTrEquipmentCreatedEvent(string name, Guid categoryId) : base(name, categoryId) { }
    }
}