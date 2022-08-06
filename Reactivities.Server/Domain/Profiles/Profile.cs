﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Authentication;
using Domain.Activities;
using Domain.Common.Base;
using Domain.Profiles.ErrorHandling;

namespace Domain.Profiles
{
    public class Profile : DomainEntity
    {
        private Profile()
        {

        }

        private Profile(string userName, string email, string displayName, string bio = null, Picture picture = null)
        {
            this.UserName = userName;
            this.Email = email;
            this.DisplayName = displayName;
            this.Bio = bio;
            this.Picture = picture;
        }

        [StringLength(256)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public Picture Picture { get; set; }

        public ICollection<Activity> AttendingActivities { get; set; } = new List<Activity>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<ProfileFollowing> Followers { get; set; } = new List<ProfileFollowing>();

        public ICollection<ProfileFollowing> Followings { get; set; } = new List<ProfileFollowing>();

        public void AddPicture(string publicId, string url)
        {
            this.Picture = new Picture
            {
                PublicId = publicId,
                Url = url
            };
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
