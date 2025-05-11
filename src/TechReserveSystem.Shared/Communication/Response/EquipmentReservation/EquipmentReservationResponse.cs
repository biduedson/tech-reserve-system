namespace TechReserveSystem.Shared.Communication.Response.EquipmentReservation
{
    public class EquipmentReservationResponse
    {
        public string UserName { get; set; } = string.Empty;
        public string EquipmentName { get; set; } = string.Empty;
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }

}