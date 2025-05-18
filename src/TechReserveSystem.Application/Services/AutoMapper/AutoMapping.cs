using AutoMapper;
using TechReserveSystem.Domain.Entities;
using TechReserveSystem.Shared.Communication.Request.Equipment;
using TechReserveSystem.Shared.Communication.Request.EquipmentCategory;
using TechReserveSystem.Shared.Communication.Request.EquipmentReservation;
using TechReserveSystem.Shared.Communication.Request.User;

namespace TechReserveSystem.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            ConfigureUserMappings();
            ConfigureEquipmentMappings();
            ConfigureReservationMappings();
        }

        private void ConfigureUserMappings()
        {
            CreateMap<RequestRegisterUserJson, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }

        private void ConfigureEquipmentMappings()
        {
            CreateMap<RequestRegisterEquipmentJson, Equipment>();
            CreateMap<EquipmentCategoryRegisterRequest, EquipmentCategory>();
        }

        private void ConfigureReservationMappings()
        {
            CreateMap<EquipmentReservationRequest, EquipmentReservation>();
        }
    }

}