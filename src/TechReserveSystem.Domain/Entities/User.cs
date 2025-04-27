namespace TechReserveSystem.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = UserRole.Regular.ToString();
        public ICollection<EquipmentReservation> EquipmentReservations { get; set; } = new List<EquipmentReservation>();
    }
}