namespace TechReserveSystem.Shared.Communication.Request.Equipment
{
    public class RequestRegisterEquipmentJson
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AvailableQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}