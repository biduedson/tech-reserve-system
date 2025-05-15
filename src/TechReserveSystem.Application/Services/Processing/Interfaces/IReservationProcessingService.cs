
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;

namespace TechReserveSystem.Application.Services.Processing.Interfaces
{
    public interface IReservationProcessingService
    {
        Task<EquipmentReservationResponse> Register(EquipmentReservationRequest request);
    }
}