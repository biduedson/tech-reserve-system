using MediatR;
using TechReserveSystem.Application.Events.Equipment;
using TechReserveSystem.Domain.Exceptions;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentCategoryRepository;
using TechReserveSystem.Domain.Interfaces.Repositories.EquipmentRepository;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.EventHandlers
{
    public class EquipmentCreatedEventHandler : INotificationHandler<MediaTrEquipmentCreatedEvent>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IEquipmentCategoryRepository _equipmentCategoryRepository;

        public EquipmentCreatedEventHandler(IEquipmentRepository equipmentRepository, IEquipmentCategoryRepository equipmentCategoryRepository)
        {
            _equipmentRepository = equipmentRepository;
            _equipmentCategoryRepository = equipmentCategoryRepository;
        }

        public async Task Handle(MediaTrEquipmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            bool isEquipmentNameValid = await _equipmentRepository.GetByName(notification.Name) != null;
            if (!isEquipmentNameValid)
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.EQUIPMENT_ALREADY_REGISTERED));

            bool isCategoryExist = await _equipmentRepository.GetById(notification.CategoryId) != null;

            if (isCategoryExist)
                throw new BusinessRuleException(ResourceAppMessages.GetExceptionMessage(NotFoundMessagesExceptions.CATEGORY_NOT_FOUND));
        }
    }
}