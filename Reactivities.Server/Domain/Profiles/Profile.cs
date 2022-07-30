using System.Collections.Generic;
using System.Security.Authentication;
using Domain.Activities;
using Domain.Activities.ErrorHandling;

namespace Domain.Profiles
{
    public class Profile
    {
        private Profile()
        {

        }

        private Profile(string userName, string email, string displayName, string bio = null, string profilePictureUrl = null)
        {
            this.UserName = userName;
            this.Email = email;
            this.DisplayName = displayName;
            this.Bio = bio;
            this.ProfilePictureUrl = profilePictureUrl;
        }

        public int Id { get; init; }

        public string UserName { get; init; }

        public string Email { get; }

        public string DisplayName { get; init; }

        public string Bio { get; init; }

        public string ProfilePictureUrl { get; init; }

        public ICollection<Activity> AttendingActivities { get; set; } = new List<Activity>();

        public static Profile New(
            string userName, string email, string displayName, string bio = null, string profilePictureUrl = null)
        {
            ValidateUser(userName, email, displayName);

            return new Profile(userName, email, displayName, bio, profilePictureUrl);
        }

        private static void ValidateUser(string userName, string email, string displayName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new InvalidCredentialException(
                    UserErrorMessages.EmailCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidCredentialException(
                    UserErrorMessages.UsernameCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new InvalidCredentialException(
                    UserErrorMessages.InvalidDisplayName);
            }
        }
    }
}
