using System;
using Domain.Activities.ErrorHandling;
using Domain.Common.Base;
using Domain.Profiles;

namespace Domain.Activities
{
    public class Comment : DomainEntity
    {
        private Comment() {}

        private Comment(string content, Profile author, DateTime? created = null)
        {
            Content = content;
            Author = author;
            CreatedAt = created ?? DateTime.UtcNow;
        }

        public string Content { get; private set; }

        public int AuthorId { get; private set; }

        public Profile Author { get; private set; }

        public int ActivityId { get; private set; }

        public Activity Activity { get; private set; }
        
        public DateTime CreatedAt { get; set; }

        public static Comment New(string content, Profile author, DateTime? created = null)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(
                    ActivityErrorMessages.EmptyContent);
            }
            
            if (author == null)
            {
                throw new ArgumentException(
                    ActivityErrorMessages.InvalidAuthor);
            }

            return new Comment(content, author, created);
        }
    }
}