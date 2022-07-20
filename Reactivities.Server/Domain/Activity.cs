using System;
using Domain.Base;

namespace Domain
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
    }
}
