using System;
using Domain.Activities.Enums;

namespace Application.Activities.Models.Base;

public abstract class CreateEditActivityBaseInputModel
{
    public string Title { get; init; }

    public DateTime Date { get; init; }

    public string Description { get; init; }

    public CategoryType CategoryType { get; init; }

    public string City { get; init; }

    public string Venue { get; init; }
        
    public int HostId { get; private set; }

    public void SetHostId(int hostId) => this.HostId = hostId;
}