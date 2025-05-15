
using TechReserveSystem.Application.Interfaces.UseCases.EquipmentReservation;
using TechReserveSystem.Application.Services.Processing.Interfaces;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Response.EquipmentReservation;


namespace TechReserveSystem.Application.UseCases.EquipmentReservation
{
    public class RegisterReservationUseCase : IRegisterReservationUseCase
    {
        private readonly IReservationProcessingService _reservationProcessingService;

        public RegisterReservationUseCase(IReservationProcessingService reservationProcessingService)
        {
            _reservationProcessingService = reservationProcessingService;
        }

        public async Task<EquipmentReservationResponse> Execute(EquipmentReservationRequest request)
        {
            var result = await _reservationProcessingService.Register(request);
            return result;
        }

    }
}