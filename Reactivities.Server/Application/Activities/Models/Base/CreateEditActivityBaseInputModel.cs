using System;
using Models.Enumerations;

namespace Application.Activities.Models.Base
{
    public abstract class CreateEditActivityBaseInputModel
    {
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public CategoryType CategoryType { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }
    }
}
