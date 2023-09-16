using Application.Pictures.Interfaces.DataServices;
using Domain.Profiles;
using Reactivities.Common.DataServices.Services;

namespace Infrastructure.Pictures.Services.DataServices;

public class PicturesDataService : EntityDataService<DataContext,Picture>, IPicturesDataService
{
    public PicturesDataService(DataContext dataContext) : base(dataContext)
    {
    }
}