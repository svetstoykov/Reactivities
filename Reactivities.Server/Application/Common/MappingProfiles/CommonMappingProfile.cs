using Application.Common.Models;
using Application.Common.Models.Pagination;
using AutoMapper;

namespace Application.Common.MappingProfiles
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
        }
    }
}
