using FluentValidation;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Exceptions.Constants;
using TechReserveSystem.Shared.Resources;

namespace TechReserveSystem.Application.Validators.EquipmentReservation
{
    public class RegisterReservationValidator : AbstractValidator<EquipmentReservationRequest>
    {
        public RegisterReservationValidator()
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
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_STARTDATE_INVALID))
            .Must(date => date.Date >= DateTime.UtcNow.Date)
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_PAST_DATE));

            RuleFor(reservation => reservation.ExpectedReturnDate)
            .NotEmpty()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_RETURN_EMPTY))
            .NotNull()
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_RETURN_INVALID))
            .Must(date => date != default(DateTime))
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_RETURN_INVALID))
            .Must(date => date.Kind == DateTimeKind.Utc || date.Kind == DateTimeKind.Local)
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_EXPECTED_RETURN_INVALID))
            .Must(date => date.Date >= DateTime.UtcNow.Date)
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_RETURN_PAST_DATE));

            RuleFor(reservation => reservation.ExpectedReturnDate)
            .Must((reservation, returnDate) => returnDate > reservation.StartDate)
                      .WithMessage(ResourceAppMessages.GetExceptionMessage(ReservationMessagesExceptions.RESERVATION_RETURN_BEFORE_START));
        }
    }
}