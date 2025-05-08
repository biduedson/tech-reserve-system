using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;

namespace TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation
{
    public interface IRegisterReservationUseCase
    {
        public Task<EquipmentReservationResponse> Execute(EquipmentReservationRequest useCase);
    }
}