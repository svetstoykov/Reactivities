using System;
using API.Common;
using Models.Common;

namespace API.Activities.Models
{
    public class ActivityViewModel : BaseApiModel
    {
        public int? Id { get; set; }
        
        public string Title { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Description { get; set; }
        
        public int CategoryId { get; set; }

        public string Category { get; set; }

        public string City { get; set; }
        
        public string Venue { get; set; }
    }
}
