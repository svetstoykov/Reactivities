using System.Linq;
using Application.Profiles.Models;
using AutoMapper;
using Domain.Activities;
using DomainEntity = Domain.Profiles;
using Domain.Activities.Enums;

namespace Application.Common.MappingProfiles
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {
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
