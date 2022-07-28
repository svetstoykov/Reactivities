using API.Common.Identity.Models;
using Application.Common.Identity.Models.Output;
using AutoMapper;

namespace API.Common.MappingProfiles
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            this.CreateMap<UserOutputModel, UserApiModel>();
            
            this.CreateMap<UserOutputModel, ProfileApiModel>();
        }
    }
}
