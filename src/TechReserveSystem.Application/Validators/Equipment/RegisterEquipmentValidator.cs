using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Application.Validators.Equipment
{
    public class RegisterEquipmentValidator : AbstractValidator<RequestRegisterEquipmentJson>
    {
        public RegisterEquipmentValidator()
        {
            RuleFor(equipment => equipment.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY)
            .NotNull().WithMessage(ResourceMessagesException.NAME_EMPTY);

            RuleFor(equipment => equipment.Description)
            .NotEmpty().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY)
            .NotNull().WithMessage(ResourceMessagesException.DESCRIPTION_EMPTY);

            RuleFor(equipment => equipment.CategoryId)
            .NotEmpty().WithMessage(ResourceMessagesException.CATEGORY_ID_EMPTY)
            .NotNull().WithMessage(ResourceMessagesException.CATEGORY_ID_EMPTY);

            RuleFor(equipment => equipment.AvailableQuantity)
            .NotEmpty().WithMessage(ResourceMessagesException.AVAILABLE_QUANTITY_EQUIPMENT_EMPTY)
            .Custom((quantity, context) =>
            {
                if (quantity < 1)
                {
                    context.AddFailure(ResourceMessagesException.AVAILABLE_QUANTITY_EQUIPMENT_INVALID);
                }
            });
        }
    }
}