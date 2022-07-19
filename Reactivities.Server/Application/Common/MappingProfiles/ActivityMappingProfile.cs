using System;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;
using Domain;
using Models.Common;

namespace Application.Common.MappingProfiles
{
    public class ActivityMappingProfile : Profile
    {

        public ActivityMappingProfile()
        {
            CreateMap<CreateActivityInputModel, Activity>()
                .ForMember(dest => dest.Date, 
                    opt => opt.MapFrom(src => DateTime.Parse(src.Date)));

            CreateMap<EditActivityInputModel, Activity>()
                .ForMember(dest => dest.Date, 
                    opt => opt.MapFrom(src => DateTime.Parse(src.Date)));

            CreateMap<Activity, ActivityOutputModel>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date.ToString(GlobalConstants.DateFormat)));
        }
    }
}
