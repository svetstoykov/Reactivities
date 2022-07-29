using System;
using System.Collections.Generic;
using Domain.Common.Base;
using Domain.Common.Identity;

namespace Domain.Activities
{
    public class Activity : DomainModel
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        
        public string HostId { get; set; }
        
        public User Host { get; set; }

        public bool IsCancelled { get; set; }

        public ICollection<User> Attendees { get; set; } = new List<User>();
    }
}
