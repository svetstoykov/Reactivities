using API.Activities.Models;
using API.Activities.Models.Comments;
using Application.Activities.Models.Base;
using Application.Activities.Models.Enums;
using Application.Activities.Models.Input;
using Application.Activities.Models.Output;
using AutoMapper;
using Domain.Activities.Enums;

namespace API.Common.MappingProfiles;

public class ActivitiesMappingProfile : Profile
{
    public ActivitiesMappingProfile()
    {
        this.CreateMap<ActivityApiModel, CreateEditActivityBaseInputModel>()
            .Include<ActivityApiModel, CreateActivityInputModel>()
            .Include<ActivityApiModel, EditActivityInputModel>()
            .ForMember(dest => dest.CategoryType,
                opt => opt.MapFrom(src => (CategoryType) src.CategoryId));

        this.CreateMap<ActivityApiModel, CreateActivityInputModel>();

        this.CreateMap<ActivityApiModel, EditActivityInputModel>();

        this.CreateMap<CategoryOutputModel, CategoryApiModel>();

        this.CreateMap<CommentOutputModel, CommentApiModel>();
            
        this.CreateMap<ActivityOutputModel, ActivityApiModel>()
            .ForMember(dest => dest.CategoryId,
                opt => opt.MapFrom(src => (int) src.CategoryType))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.CategoryType.ToString()));
    }
}