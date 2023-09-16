using Domain.Activities.Enums;
using Domain.Common.Base;

namespace Domain.Activities;

public class Category : DomainEntity
{
    private Category(){}

    private Category(int id,string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public string Name { get; private set; }

    public static Category New(CategoryType categoryType) 
        => new((int) categoryType, categoryType.ToString());
}