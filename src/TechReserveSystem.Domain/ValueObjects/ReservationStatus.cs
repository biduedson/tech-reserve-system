namespace TechReserveSystem.Domain.ValueObjects
{
    public class ReservationStatus
    {
        public string Status { get; }

        private ReservationStatus(string status) => Status = status;

        public static readonly ReservationStatus Pending = new("Pending");
        public static readonly ReservationStatus Approved = new("Approved");
        public static readonly ReservationStatus Rejected = new("Rejected");
        public static readonly ReservationStatus InProgress = new("InProgress");
        public static readonly ReservationStatus Completed = new("Completed");
        public static readonly ReservationStatus Cancelled = new("Cancelled");

        public override string ToString() => Status;
    }
}