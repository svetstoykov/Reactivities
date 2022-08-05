using Application.Pictures;
using Domain.Profiles;
using Persistence.Common.DataServices;

namespace Persistence.Pictures;

public class PicturesesDataService : EntityDataService<Picture>, IPicturesDataService
{
    public PicturesesDataService(DataContext dataContext) : base(dataContext)
    {
    }
}