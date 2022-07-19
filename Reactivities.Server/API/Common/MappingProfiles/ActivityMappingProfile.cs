using API.Activities.Models;
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
            CreateMap<ActivityViewModel, CreateEditActivityBaseInputModel>()
                .Include<ActivityViewModel, CreateActivityInputModel>()
                .Include<ActivityViewModel, EditActivityInputModel>()
                .ForMember(dest => dest.CategoryType,
                    opt => opt.MapFrom(src => (CategoryType) src.CategoryId));

            CreateMap<ActivityViewModel, CreateActivityInputModel>();

            CreateMap<ActivityViewModel, EditActivityInputModel>();

            CreateMap<CategoryOutputModel, CategoryViewModel>();

            CreateMap<ActivityOutputModel, ActivityViewModel>()
                .ForMember(dest => dest.CategoryId,
                    opt => opt.MapFrom(src => (int) src.CategoryType))
                .ForMember(dest => dest.Category,
                    opt => opt.MapFrom(src => src.CategoryType.ToString()));
        }
    }
}
