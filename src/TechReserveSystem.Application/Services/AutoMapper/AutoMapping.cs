using AutoMapper;
using TechReserveSystem.Domain.Entities;
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
        }
    }
}