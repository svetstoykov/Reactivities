using System;
using API.Common.Models;

namespace API.Profiles.Models;

public class ProfileActivityApiModel : BaseApiModel
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime Date { get; set; }

    public string Category { get; set; }
}