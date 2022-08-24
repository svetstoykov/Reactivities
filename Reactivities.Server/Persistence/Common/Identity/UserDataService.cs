using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.ErrorHandling;

namespace Persistence.Common.Identity
{
    public class UserDataService : IUserDataService
    {
        private readonly UserManager<User> _userManager;

        public UserDataService(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<bool> ChangeEmailAddress(string currentEmail, string newEmail)
        {
            var user = await _userManager.FindByEmailAsync(currentEmail);

            var updateResult = await _userManager.SetEmailAsync(user, newEmail);
            if (updateResult.Succeeded)
            {
                return true;
            }

            throw new AppException(string.Join(", ", updateResult.Errors));
        }
    }
}