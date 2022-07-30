using System;
using System.Collections.Generic;
using API.Common;
using API.Profiles.Models;

namespace API.Activities.Models
{
    public class ActivityApiModel : BaseApiModel
    {
        public int? Id { get; set; }
        
        public string Title { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Description { get; set; }
        
        public int CategoryId { get; set; }

        public string Category { get; set; }

        public string City { get; set; }
        
        public string Venue { get; set; }
        
        public ProfileApiModel Host { get; set; }
        
        public bool IsCancelled { get; set; }

        public ICollection<ProfileApiModel> Attendees { get; set; } = new List<ProfileApiModel>();
    }
}
