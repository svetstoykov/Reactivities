using API.Profiles.Models;
using Application.Profiles.Models;
using AutoMapper;

namespace API.Common.MappingProfiles
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {
            this.CreateMap<ProfileOutputModel, ProfileApiModel>();
        }
    }
}
