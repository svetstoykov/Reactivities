using System.Threading.Tasks;

namespace Application.Common.Identity.DataServices
{
    public interface IUserDataService
    {
        Task<bool> ChangeEmailAddress(string currentEmail, string newEmail);
    }
}
