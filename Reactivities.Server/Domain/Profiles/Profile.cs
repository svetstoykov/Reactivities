using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Domain.Activities;
using Domain.Common.Base;
using Domain.Profiles.ErrorHandling;

namespace Domain.Profiles
{
    public class Profile : DomainEntity
    {
        private readonly List<Activity> _attendingActivities = new();
        private readonly List<Comment> _comments = new();
        private readonly List<ProfileFollowing> _followers = new();
        private readonly List<ProfileFollowing> _followings = new();

        private Profile() {}
        private Profile(string userName, string email, string displayName, string bio = null, Picture picture = null)
        {
            this.UserName = userName;
            this.Email = email;
            this.DisplayName = displayName;
            this.Bio = bio;
            this.Picture = picture;
        }

        [StringLength(256)]
        public string UserName { get; private set; }

        [StringLength(256)]
        public string Email { get; private set; }

        public string DisplayName { get; private set; }

        public string Bio { get; private set; }

        public Picture Picture { get; private set; }

        public IReadOnlyCollection<Activity> AttendingActivities => this._attendingActivities.AsReadOnly();

        public IReadOnlyCollection<Comment> Comments => this._comments.AsReadOnly();

        public IReadOnlyCollection<ProfileFollowing> Followers => this._followers.AsReadOnly();

        public IReadOnlyCollection<ProfileFollowing> Followings => this._followings.AsReadOnly();

        public void RemoveFollowing(ProfileFollowing following)
            => this._followings.Remove(following);

        public void AddFollowing(ProfileFollowing following)
            => this._followings.Add(following);

        public void AddPicture(string publicId, string url)
            => this.Picture = Picture.New(publicId, url);

        public void UpdatePersonalInfo(string bio, string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentException(
                    ProfileErrorMessages.InvalidDisplayName);
            }

            this.Bio = bio;
            this.DisplayName = displayName;
        }

        public void UpdateEmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(
                    ProfileErrorMessages.EmailCannotBeEmpty);
            }

            this.Email = email;
        }

        public static Profile New(
            string userName, string email, string displayName, string bio = null, Picture picture = null)
        {
            ValidateProfile(userName, email, displayName);

            return new Profile(userName, email, displayName, bio, picture);
        }

        private static void ValidateProfile(string userName, string email, string displayName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new InvalidCredentialException(
                    ProfileErrorMessages.EmailCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidCredentialException(
                    ProfileErrorMessages.UsernameCannotBeEmpty);
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new InvalidCredentialException(
                    ProfileErrorMessages.InvalidDisplayName);
            }
        }
    }
}
