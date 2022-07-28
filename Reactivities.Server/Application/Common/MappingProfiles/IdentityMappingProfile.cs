using Application.Common.Identity.Models;
using Application.Common.Identity.Models.Output;
using AutoMapper;

namespace Application.Common.MappingProfiles
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            this.CreateMap<User, UserOutputModel>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.UserName));
        }
    }
}
