using TechReserveSystem.Domain.Enuns;

namespace TechReserveSystem.Domain.Entities
{
    public class EquipmentReservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string Status { get; set; } = ReservationStatus.Pending.ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public Equipment Equipment { get; set; } = null!;
    }

}