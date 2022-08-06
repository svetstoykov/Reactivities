using System;
using Domain.Activities.ErrorHandling;
using Domain.Common.Base;
using Domain.Profiles;

namespace Domain.Activities
{
    public class Comment : DomainEntity
    {
        public string Content { get; set; }

        public int AuthorId { get; set; }

        public Profile Author { get; set; }

        public int ActivityId { get; set; }

        public Activity Activity { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public static Comment New(string content, Profile author, DateTime? created = null)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(
                    CommentErrorMessages.EmptyContent);
            }
            
            if (author == null)
            {
                throw new ArgumentException(
                    CommentErrorMessages.InvalidAuthor);
            }

            return new Comment
            {
                Content = content,
                Author = author,
                CreatedAt = created ?? DateTime.UtcNow
            };
        }
    }
}