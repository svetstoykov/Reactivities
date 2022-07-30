using Application.Profiles.Models;
using AutoMapper;
using User = Application.Common.Identity.Models.User;

namespace Application.Common.MappingProfiles
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {

            this.CreateMap<User, ProfileOutputModel>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.UserName));
        }
    }
}
