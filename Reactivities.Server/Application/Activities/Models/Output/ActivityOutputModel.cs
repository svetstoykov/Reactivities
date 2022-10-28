
using System.Collections.Generic;
using Application.Profiles.Models;
using Application.Profiles.Models.Output;
using Domain.Activities.Enums;

namespace Application.Activities.Models.Output;

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

    public ProfileOutputModel Host { get; set; }

    public ICollection<ProfileOutputModel> Attendees { get; set; } = new List<ProfileOutputModel>();
}