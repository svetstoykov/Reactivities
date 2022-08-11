using System;
using Application.Activities.Models.Enums;
using Domain.Activities.Enums;

namespace Application.Profiles.Models
{
    public class ProfileActivityOutputModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public CategoryType Category { get; set; }
    }
}
