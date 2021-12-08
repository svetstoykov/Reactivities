using System.Globalization;
using AutoMapper;
using Domain;
using Models.Activities.Request;
using Models.Activities.Response;

namespace Application.Common.Activities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateActivityRequest, Activity>();
            CreateMap<EditActivityRequest, Activity>();
            CreateMap<Activity, ActivityResponse>()
                .ForMember(x => x.Date, 
                              y => y.MapFrom(activity => activity.Date.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
