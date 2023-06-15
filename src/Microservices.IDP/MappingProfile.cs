using AutoMapper;
using Microservices.IDP.Infrastructure.Entities;
using Microservices.IDP.Infrastructure.ViewModels;

namespace Microservices.IDP
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Permission, PermissionUserViewModel>();
        }

    }
}
