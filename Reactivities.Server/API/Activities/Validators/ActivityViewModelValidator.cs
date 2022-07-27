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
            RuleFor(m => m.Title).NotEmpty();

            var lastCategoryType = Enum.GetValues<CategoryType>().MaxBy(c => (int) c);
            RuleFor(m => m.CategoryId).GreaterThan(0).LessThan((int)lastCategoryType);

            RuleFor(m => m.City).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
            RuleFor(m => m.Venue).NotEmpty();
            RuleFor(m => m.Date).GreaterThan(DateTime.MinValue);
        }
    }
}
