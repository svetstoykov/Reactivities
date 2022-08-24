using Application.Pictures;
using Domain.Profiles;
using Infrastructure.Common.DataServices;

namespace Infrastructure.Pictures
{
    public class PicturesDataService : EntityDataService<Picture>, IPicturesDataService
    {
        public PicturesDataService(DataContext dataContext) : base(dataContext)
        {
        }
    }
}