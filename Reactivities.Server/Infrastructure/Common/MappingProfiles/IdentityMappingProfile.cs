using Application.Profiles.Models.Output;
using AutoMapper;
using Infrastructure.Identity.Models;

namespace Infrastructure.Common.MappingProfiles;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        this.CreateMap<User, ProfileOutputModel>()
            .ForMember(dest => dest.Username,
                opt => opt.MapFrom(src => src.UserName));
    }
}