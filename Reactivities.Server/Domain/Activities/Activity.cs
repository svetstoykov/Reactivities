using System;
using System.Collections.Generic;
using Domain.Activities.ErrorHandling;
using Domain.Common.Base;
using Domain.Profiles;

namespace Domain.Activities
{
    public class Activity : DomainEntity
    {
        private readonly List<Profile> _attendees = new();

        private readonly List<Comment> _comments = new();

        private Activity() { }

        private Activity(string title, DateTime date, string description, string city, string venue, int categoryId, int hostId)
        {
            this.Title = title;
            this.Date = date;
            this.Description = description;
            this.City = city;
            this.Venue = venue;
            this.CategoryId = categoryId;
            this.HostId = hostId;
            this.IsCancelled = false;
        }

        public string Title { get; private set; }

        public DateTime Date { get; private set; }

        public string Description { get; private set; }

        public string City { get; private set; }

        public string Venue { get; private set; }

        public int CategoryId { get; private set; }

        public Category Category { get; private set; }

        public int HostId { get; private set; }

        public Profile Host { get; private set; }

        public bool IsCancelled { get; private set; }

        public IReadOnlyCollection<Profile> Attendees => this._attendees.AsReadOnly();

        public IReadOnlyCollection<Comment> Comments => this._comments.AsReadOnly();

        public void AddAttendee(Profile attendee)
            =>
                this._attendees.Add(attendee);

        public bool RemoveAttendee(Profile attendee)
            => this._attendees.Remove(attendee);

        public void AddComment(Comment comment)
            => this._comments.Add(comment);

        public void UpdateIsCancelledStatus(bool state)
            => this.IsCancelled = state;

        public void UpdateDetails(string title, DateTime date, string description, string city, string venue, int categoryId)
        {
            ValidateActivity(title, description, city, venue, categoryId);

            this.Title = title;
            this.Description = description;
            this.Date = date;
            this.City = city;
            this.Venue = venue;
            this.CategoryId = categoryId;
        }

        public static Activity New(string title, DateTime date, string description, string city, string venue, int categoryId, int hostId)
        {
            ValidateActivity(title, description, city, venue, categoryId);

            if (hostId <= 0)
            {
                throw new ArgumentException(
                    ActivityErrorMessages.InvalidHost);
            }

            return new Activity(title, date, description, city, venue, categoryId, hostId);
        }

        private static void ValidateActivity(string title, string description, string city, string venue, int categoryId)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException(
                    ActivityErrorMessages.EmptyTitle);
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException(
                    ActivityErrorMessages.EmptyDescription);
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentException(
                    ActivityErrorMessages.EmptyCity);
            }

            if (string.IsNullOrEmpty(venue))
            {
                throw new ArgumentException
                    (ActivityErrorMessages.EmptyVenue);
            }

            if (categoryId <= 0)
            {
                throw new ArgumentException(
                    ActivityErrorMessages.InvalidCategory);
            }
        }
    }
}
