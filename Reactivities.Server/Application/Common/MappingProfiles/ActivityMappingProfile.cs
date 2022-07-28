﻿using Application.Activities.Models.Base;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;
using Domain;
using Models.Enumerations;

namespace Application.Common.MappingProfiles
{
    public class ActivityMappingProfile : Profile
    {

        public ActivityMappingProfile()
        {
            this.CreateMap<CreateActivityInputModel, Activity>();

            this.CreateMap<EditActivityInputModel, Activity>();

            this.CreateMap<Category, CategoryOutputModel>();

            this.CreateMap<CreateEditActivityBaseInputModel, Activity>()
                .Include<CreateActivityInputModel, Activity>()
                .Include<EditActivityInputModel, Activity>()
                .ForMember(dest => dest.CategoryId,
                    opt => opt.MapFrom(src => (int)src.CategoryType));

            this.CreateMap<Activity, ActivityOutputModel>()
                .ForMember(dest => dest.CategoryType,
                    opt => opt.MapFrom(src => (CategoryType) src.CategoryId));
        }
    }
}
