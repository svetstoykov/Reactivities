using System.Collections.Generic;
using System.Security.Authentication;
using Microsoft.AspNetCore.Identity;
using Domain.Activities;

namespace Domain.Common.Identity
{
    public class User : IdentityUser
    {
        private const string EmailCannotBeEmpty = "Email cannot be empty.";

        private const string UsernameCannotBeEmpty = "Username cannot be empty.";

        private const string InvalidDisplayName = "Display name cannot be empty.";
        
        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public ICollection<Activity> AttendingActivities { get; set; } = new List<Activity>();

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
                    EmailCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidCredentialException(
                    UsernameCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new InvalidCredentialException(
                    InvalidDisplayName);
            }
        }
    }
}
