using TechReserveSystem.Domain.Constants;
using TechReserveSystem.Domain.Exceptions;

namespace TechReserveSystem.Domain.ValueObjects
{
    public class ReservationPeriod
    {
        public DateTime StartDate { get; }
        public DateTime ExpectedReturnDate { get; }

        public ReservationPeriod(DateTime startDate, DateTime expectedReturnDate)
        {
            EnsureRules(startDate, expectedReturnDate);
            StartDate = startDate;
            ExpectedReturnDate = expectedReturnDate;
        }

        private void EnsureRules(DateTime startDate, DateTime expectedReturnDate)
        {
            var exceptionMessage = (startDate, expectedReturnDate) switch
            {
                _ when startDate.Date < DateTime.UtcNow.Date => ReservationPeriodErrorMessages.RESERVATION_PAST_DATE,
                _ when startDate.Date < DateTime.UtcNow.Date.AddDays(1) => ReservationPeriodErrorMessages.RESERVATION_DATE_TOO_EARLY,
                _ when startDate.Date > DateTime.UtcNow.Date.AddDays(30) => ReservationPeriodErrorMessages.RESERVATION_MAX_ADVANCE_LIMIT,
                _ when expectedReturnDate.Date < DateTime.UtcNow.Date => ReservationPeriodErrorMessages.RESERVATION_RETURN_PAST_DATE,
                _ when expectedReturnDate.Date < startDate.Date => ReservationPeriodErrorMessages.RESERVATION_RETURN_BEFORE_START,
                _ when expectedReturnDate.Date > startDate.Date => ReservationPeriodErrorMessages.RESERVATION_MUST_BE_SAME_DAY,
                _ => null
            };

            if (exceptionMessage is not null)
                throw new BusinessRuleException(exceptionMessage);
        }
    }
}