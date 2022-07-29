﻿using API.Activities.Models;
using Application.Activities.Models.Base;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;
using Models.Enumerations;

namespace API.Common.MappingProfiles
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            this.CreateMap<ActivityApiModel, CreateEditActivityBaseInputModel>()
                .Include<ActivityApiModel, CreateActivityInputModel>()
                .Include<ActivityApiModel, EditActivityInputModel>()
                .ForMember(dest => dest.CategoryType,
                    opt => opt.MapFrom(src => (CategoryType) src.CategoryId));

            this.CreateMap<ActivityApiModel, CreateActivityInputModel>();

            this.CreateMap<ActivityApiModel, EditActivityInputModel>();

            this.CreateMap<CategoryOutputModel, CategoryApiModel>();

            this.CreateMap<ActivityOutputModel, ActivityApiModel>()
                .ForMember(dest => dest.CategoryId,
                    opt => opt.MapFrom(src => (int) src.CategoryType))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.CategoryType.ToString()))
                .ForMember(dest => dest.HostName,
                    opt => opt.MapFrom(src => src.Host.DisplayName));
        }
    }
}
