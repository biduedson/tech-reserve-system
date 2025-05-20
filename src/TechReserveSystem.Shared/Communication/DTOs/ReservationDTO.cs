namespace TechReserveSystem.Shared.Communication.DTOs
{
    public record ReservationDTO(
    Guid Id,
    UserDTO User,
    string EquipmentName,
    int Quantity,
    DateTime StartDate,
    DateTime ExpectedReturnDate,
    string Status,
    DateTime CreatedAt);
}