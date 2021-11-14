using AutoMapper;
using Domain;
using Models.Activities;

namespace Application.Common.Activities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateActivityRequest, Activity>();
            CreateMap<EditActivityRequest, Activity>();
        }
    }
}
