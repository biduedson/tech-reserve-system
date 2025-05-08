using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Exceptions.Resources;

namespace TechReserveSystem.Application.Validators.EquipmentReservation
{
    public class RegisterReservationValidator : AbstractValidator<EquipmentReservationRequest>
    {
        public RegisterReservationValidator()
        {
            RuleFor(reservation => reservation.UserId)
            .NotEmpty()
                      .WithMessage(ResourceMessagesException.USER_ID_EMPTY)
            .NotNull()
                      .WithMessage(ResourceMessagesException.USER_ID_INVALID);

            RuleFor(reservation => reservation.EquipmentId)
            .NotEmpty()
                      .WithMessage(ResourceMessagesException.EQUIPMENT_ID_EMPTY)
            .NotNull()
                      .WithMessage(ResourceMessagesException.EQUIPMENT_ID_INVALID);

            RuleFor(reservation => reservation.StartDate)
            .NotEmpty()
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_STARTDATE_EMPTY)
            .NotNull()
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_STARTDATE_INVALID)
            .Must(date => date != default(DateTime))
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_STARTDATE_INVALID)
            .Must(date => date.Kind == DateTimeKind.Utc || date.Kind == DateTimeKind.Local)
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_STARTDATE_INVALID)
            .Must(date => date.Date >= DateTime.UtcNow.Date)
                      .WithMessage(ResourceMessagesException.RESERVATION_PAST_DATE);

            RuleFor(reservation => reservation.ExpectedReturnDate)
            .NotEmpty()
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_RETURN_EMPTY)
            .NotNull()
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_RETURN_INVALID)
            .Must(date => date != default(DateTime))
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_RETURN_INVALID)
            .Must(date => date.Kind == DateTimeKind.Utc || date.Kind == DateTimeKind.Local)
                      .WithMessage(ResourceMessagesException.RESERVATION_EXPECTED_RETURN_INVALID)
            .Must(date => date.Date >= DateTime.UtcNow.Date)
                      .WithMessage(ResourceMessagesException.RESERVATION_RETURN_PAST_DATE);

            RuleFor(reservation => reservation.ExpectedReturnDate)
            .Must((reservation, returnDate) => returnDate > reservation.StartDate)
                      .WithMessage(ResourceMessagesException.RESERVATION_RETURN_BEFORE_START);
        }
    }
}