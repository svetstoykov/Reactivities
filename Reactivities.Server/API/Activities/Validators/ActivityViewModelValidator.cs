using System;
using System.Linq;
using API.Activities.Models;
using FluentValidation;
using Models.Enumerations;

namespace API.Activities.Validators
{
    public class ActivityViewModelValidator : AbstractValidator<ActivityApiModel>
    {
        public ActivityViewModelValidator()
        {
            this.RuleFor(m => m.Title).NotEmpty();

            var lastCategoryType = Enum.GetValues<CategoryType>().MaxBy(c => (int) c);
            this.RuleFor(m => m.CategoryId).GreaterThan(0).LessThan((int)lastCategoryType);

            this.RuleFor(m => m.City).NotEmpty();
            this.RuleFor(m => m.Description).NotEmpty();
            this.RuleFor(m => m.Venue).NotEmpty();
            this.RuleFor(m => m.Date).GreaterThan(DateTime.MinValue);
        }
    }
}
