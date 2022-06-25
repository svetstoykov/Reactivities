using API.Models.Common;

namespace API.Models.Activities
{
    public class ActivityViewModel : BaseApiModel
    {
        public int? Id { get; set; }
        
        public string Title { get; set; }
        
        public string Date { get; set; }
        
        public string Description { get; set; }
        
        public string Category { get; set; }
        
        public string City { get; set; }
        
        public string Venue { get; set; }
    }
}
