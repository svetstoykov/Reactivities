using System;
using Domain.Activities.Enums;

namespace Application.Profiles.Models.Output;

public class ProfileActivityOutputModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public CategoryType Category { get; set; }
}