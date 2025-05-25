
using TechReserveSystem.Domain.Constants;
using TechReserveSystem.Domain.Exceptions;
using TechReserveSystem.Domain.ValueObjects;

namespace TechReserveSystem.Domain.Aggregates
{
    public class UserAggregate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Role { get; private set; }
        private List<ReservationAggregate> _reservations = new();
        public IReadOnlyCollection<ReservationAggregate> Reservations => _reservations.AsReadOnly();

        public UserAggregate(Guid id, string name, string email, bool isActive, DateTime createdAt, string role, List<ReservationAggregate> reservations)
        {
            Id = id;
            Name = name;
            Email = email;
            IsActive = isActive;
            CreatedAt = createdAt;
            Role = role;
            _reservations = reservations;
        }

        public void AddReservation(ReservationAggregate reservation)
        {
            ValidateReservationBeforeAdd(reservation);
            _reservations.Add(reservation);
        }
        public void SetReservations(IReadOnlyCollection<ReservationAggregate> reservations)
        {
            _reservations = reservations.ToList();
        }
        private bool HasPendingReservations()
        {
            return _reservations.Any(r => r.Status == ReservationStatus.Pending);
        }

        private bool HasRejectedReservationForEquipmentOnDate(DateTime startdate, Guid equipmentId)
        {
            return _reservations.Any(r => r.Status == ReservationStatus.Rejected && r.EquipmentId == equipmentId && r.Period.StartDate == startdate.Date);
        }

        private void ValidateReservationBeforeAdd(ReservationAggregate reservation)
        {
            if (HasPendingReservations())
                throw new BusinessRuleException(ReservationErrorMessages.UNRETURNED_EQUIPMENT_RESTRICTION);

            if (HasRejectedReservationForEquipmentOnDate(reservation.Period.StartDate, reservation.EquipmentId))
                throw new BusinessRuleException(ReservationErrorMessages.RESERVATION_ALREADY_REJECTED_ON_DATE);
        }
    }

}