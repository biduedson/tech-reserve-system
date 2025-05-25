namespace TechReserveSystem.Infrastructure.Data.ViewModels
{
    public class UserReservationViewModel
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public Guid EquipmentId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
    }
}