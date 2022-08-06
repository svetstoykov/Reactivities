using System;
using System.Collections.Generic;
using Domain.Common.Base;
using Domain.Profiles;

namespace Domain.Activities
{
    public class Activity : DomainEntity
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        
        public int HostId { get; set; }
        
        public Profile Host { get; set; }

        public bool IsCancelled { get; set; }

        public ICollection<Profile> Attendees { get; set; } = new List<Profile>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
