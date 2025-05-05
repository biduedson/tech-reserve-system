using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Application.UseCases.Equipment.Validators
{
    public class RegisterEquipmentValidator : AbstractValidator<RequestRegisterEquipmentJson>
    {
        public RegisterEquipmentValidator()
        {
            RuleFor(equipment => equipment.Name).NotEmpty().NotNull().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(equipment => equipment.Description).NotEmpty().NotNull().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);
            RuleFor(equipment => equipment.CategoryId).NotEmpty().NotNull().WithMessage(ResourceMessagesException.CATEGORY_ID_EMPTY);
            RuleFor(equipment => equipment.AvailableQuantity).NotEmpty().Custom((quantity, context) =>
            {
                if (quantity < 1)
                {
                    context.AddFailure(ResourceMessagesException.AVAILABLE_QUANTITY_EQUIPMENT_INVALID);
                }
            });
        }
    }
}