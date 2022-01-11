using Models.Common;

namespace Models.Activities.Base
{
    public abstract class ActivityBaseRequest : BaseApiModel
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
    }
}
