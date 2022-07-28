using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Models.ErrorHandling.Helpers;

namespace Application.Common.Identity.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public static User New(string userName, string email, string displayName, string bio = null)
        {
            User.ValidateUser(userName, email, displayName);

            return new User
            {
                UserName = userName,
                Email = email,
                DisplayName = displayName,
                Bio = bio
            };
        }

        private static void ValidateUser(string userName, string email, string displayName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new InvalidCredentialException(
                    IdentityErrorMessages.EmailCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidCredentialException(
                    IdentityErrorMessages.UsernameCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new InvalidCredentialException(
                    IdentityErrorMessages.InvalidDisplayName);
            }
        }
    }
}
