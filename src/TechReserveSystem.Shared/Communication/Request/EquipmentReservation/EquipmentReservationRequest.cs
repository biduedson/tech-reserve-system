namespace TechReserveSystem.Shared.Communication.Request.EquipmentReservation
{
    public record EquipmentReservationRequest(
     Guid UserId,
     Guid EquipmentId,
     int Quantity,
     DateTime StartDate
    );
}