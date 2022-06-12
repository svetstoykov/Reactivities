using API.Models.Activities.Request;
using API.Models.Activities.Response;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;

namespace API.Common.MappingProfiles
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<CreateActivityRequestModel, CreateActivityInputModel>();

            CreateMap<EditActivityRequestModel, EditActivityInputModel>();

            CreateMap<ActivityOutputModel, ActivityViewModel>();
        }
    }
}
