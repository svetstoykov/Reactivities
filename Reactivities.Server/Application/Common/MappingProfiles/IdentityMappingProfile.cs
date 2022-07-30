using Application.Common.Identity.Models.Output;
using AutoMapper;
using User = Application.Common.Identity.Models.Base.User;

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
