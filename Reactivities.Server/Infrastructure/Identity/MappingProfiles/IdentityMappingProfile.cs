
using AutoMapper;
using Infrastructure.Identity.Models;

namespace Infrastructure.Identity.MappingProfiles;

public class IdentityMappingProfile : Profile
{
    public IdentityMappingProfile()
    {
        this.CreateMap<Domain.Profiles.Profile, User>();
    }
}