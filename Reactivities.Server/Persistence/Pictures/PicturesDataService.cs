using Application.Pictures;
using Domain.Profiles;
using Persistence.Common.DataServices;

namespace Persistence.Pictures;

public class PicturesDataService : EntityDataService<Picture>, IPicturesDataService
{
    public PicturesDataService(DataContext dataContext) : base(dataContext)
    {
    }
}