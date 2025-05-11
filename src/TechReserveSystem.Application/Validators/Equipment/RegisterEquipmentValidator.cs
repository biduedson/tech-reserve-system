using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Validators.Equipment
{
    public class RegisterEquipmentValidator : AbstractValidator<RequestRegisterEquipmentJson>
    {
        public RegisterEquipmentValidator()
        {
            RuleFor(equipment => equipment.Name)
            .NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.EQUIPMENT_NAME_EMPTY));

            RuleFor(equipment => equipment.Description)
            .NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.DESCRIPTION_EMPTY));


            RuleFor(equipment => equipment.CategoryId)
            .NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(CategoryMessagesExceptions.CATEGORY_ID_EMPTY));


            RuleFor(equipment => equipment.AvailableQuantity)
            .NotEmpty().WithMessage(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.AVAILABLE_QUANTITY_EQUIPMENT_EMPTY))
            .Custom((quantity, context) =>
            {
                if (quantity < 1)
                {
                    context.AddFailure(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.AVAILABLE_QUANTITY_EQUIPMENT_INVALID));
                }
            });
        }
    }
}