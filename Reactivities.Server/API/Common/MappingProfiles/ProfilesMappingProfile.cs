using API.Profiles.Models;
using Application.Common.Identity.Models.Output;
using AutoMapper;

namespace API.Common.MappingProfiles
{
    public class ProfilesMappingProfile : Profile
    {
        public ProfilesMappingProfile()
        {
            this.CreateMap<UserOutputModel, ProfileApiModel>();
        }
    }
}
