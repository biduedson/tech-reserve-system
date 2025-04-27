namespace TechReserveSystem.Domain.Enuns
{
    public enum ReservationStatus
    {
        Pending,     // Reserva em análise
        Approved,    // Reserva aprovada
        Rejected,    // Reserva rejeitada
        InProgress,  // Reserva em uso
        Completed,   // Reserva finalizada
        Cancelled    // Reserva cancelada
    }
}