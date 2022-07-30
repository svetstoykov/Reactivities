
using System.Collections.Generic;
using Application.Common.Identity.Models.Output;
using Models.Enumerations;

namespace Application.Activities.Models.Output
{
    public class ActivityOutputModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Date { get; set; }
        
        public string Description { get; set; }
        
        public CategoryType CategoryType { get; set; }
        
        public string City { get; set; }
        
        public string Venue { get; set; }
        
        public bool IsCancelled { get; set; }

        public UserOutputModel Host { get; set; }

        public ICollection<UserOutputModel> Attendees { get; set; } = new List<UserOutputModel>();
    }
}
