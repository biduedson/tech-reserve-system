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
            RequestToDomain();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<RequestRegisterEquipmentJson, Equipment>();
            CreateMap<EquipmentCategoryRegisterRequest, EquipmentCategory>();
            CreateMap<EquipmentReservationRequest, EquipmentReservation>();
        }
    }
}