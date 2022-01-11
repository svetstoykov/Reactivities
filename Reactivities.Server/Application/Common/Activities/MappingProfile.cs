using System;
using System.Globalization;
using AutoMapper;
using Domain;
using Models.Activities.Request;
using Models.Activities.Response;
using Models.Common;

namespace Application.Common.Activities
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateActivityRequest, Activity>()
                .ForMember(x => x.Date, y => y.MapFrom(request => DateTime.Parse(request.Date)));
            CreateMap<EditActivityRequest, Activity>()
                .ForMember(x => x.Date, y => y.MapFrom(request => DateTime.Parse(request.Date)));
            CreateMap<Activity, ActivityResponse>()
                .ForMember(x => x.Date, 
                              y => y.MapFrom(activity => activity.Date.ToString(GlobalConstants.DateFormat)));
        }
    }
}
