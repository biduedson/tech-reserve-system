using FluentValidation;
using TechReserveSystem.Application.Validations.Reservation.interfaces;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Exceptions.ExceptionsBase.Validation;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Validations.Reservation.Implementations
{
    public class ReservationValidation : AbstractValidator<EquipmentReservationRequest>, IReservationValidation
    {
        public ReservationValidation()
        {
            RuleFor(reservation => reservation.UserId)
            .NotEmpty()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.USER_ID_EMPTY))
            .NotNull()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(UserMessagesExceptions.USER_ID_INVALID));

            RuleFor(reservation => reservation.EquipmentId)
            .NotEmpty()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.EQUIPMENT_ID_EMPTY))
            .NotNull()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(EquipmentMessagesExceptions.EQUIPMENT_ID_INVALID));

            RuleFor(reservation => reservation.StartDate)
            .NotEmpty()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_STARTDATE_EMPTY))
            .NotNull()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_STARTDATE_INVALID))
            .Must(date => date != default(DateTime))
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_STARTDATE_INVALID))
            .Must(date => date.Kind == DateTimeKind.Utc || date.Kind == DateTimeKind.Local)
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_STARTDATE_INVALID));
        }

        public void Validation(EquipmentReservationRequest request)
        {
            var result = Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}