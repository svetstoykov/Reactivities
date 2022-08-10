using System.Linq;
using Application.Activities.Models.Enums;
using Application.Profiles.Models;
using AutoMapper;
using Domain.Activities;
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

            this.CreateMap<Activity, ProfileActivityOutputModel>()
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => (CategoryType)src.CategoryId));

            string currentProfile = null;
            this.CreateMap<DomainEntity.Profile, ProfileOutputModel>()
                .ForMember(dest => dest.FollowersCount,
                    opt => opt.MapFrom(src => src.Followers.Count))
                .ForMember(dest => dest.FollowingsCount,
                    opt => opt.MapFrom(src => src.Followings.Count))
                .ForMember(dest => dest.Following,
                    opt => opt.MapFrom(src => src.Followers.Any(f => f.Observer.UserName == currentProfile)))
                .ForMember(dest => dest.PictureUrl,
                    opt => opt.MapFrom(src => src.Picture == null 
                        ? string.Empty 
                        : src.Picture.Url));
        }
    }
}
