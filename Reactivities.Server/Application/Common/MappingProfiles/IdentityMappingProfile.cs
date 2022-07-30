using Application.Common.Identity.Models.Output;
using AutoMapper;
using User = Application.Common.Identity.Models.User;

namespace Application.Common.MappingProfiles
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            this.CreateMap<User, UserOutputModel>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DisplayName,
                    opt => opt.MapFrom(src => src.Profile.DisplayName))
                .ForMember(dest => dest.ProfileImage,
                    opt => opt.MapFrom(src => src.Profile.ProfilePictureUrl));

            this.CreateMap<Profile, User>()
                .ForMember(dest => dest.Profile,
                    opt => opt.MapFrom(src => src));
        }
    }
}
