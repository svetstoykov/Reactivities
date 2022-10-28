using Application.Pictures.Interfaces.DataServices;
using Domain.Profiles;
using Infrastructure.Common.DataServices;

namespace Infrastructure.Pictures.Services.DataServices;

public class PicturesDataService : EntityDataService<Picture>, IPicturesDataService
{
    public PicturesDataService(DataContext dataContext) : base(dataContext)
    {
    }
}