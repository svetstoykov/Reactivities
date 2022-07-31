using Application.Profiles.Models;
using AutoMapper;
using User = Application.Common.Identity.Models.User;
using DomainEntity = Domain.Profiles;

namespace Application.Common.MappingProfiles
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {

            this.CreateMap<User, ProfileOutputModel>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.UserName));

            this.CreateMap<DomainEntity.Profile, ProfileOutputModel>()
                .ForMember(dest => dest.ProfilePictureUrl,
                    opt => opt.MapFrom(src => src.Picture == null 
                        ? string.Empty 
                        : src.Picture.Url));
        }
    }
}
