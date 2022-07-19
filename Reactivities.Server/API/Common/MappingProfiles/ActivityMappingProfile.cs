using API.Activities.Models;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;

namespace API.Common.MappingProfiles
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<ActivityViewModel, CreateActivityInputModel>();

            CreateMap<ActivityViewModel, EditActivityInputModel>();

            CreateMap<ActivityOutputModel, ActivityViewModel>();
        }
    }
}
