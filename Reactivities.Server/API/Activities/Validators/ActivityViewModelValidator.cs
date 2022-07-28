using System;
using System.Linq;
using API.Activities.Models;
using FluentValidation;
using Models.Enumerations;
using Models.ErrorHandling.Helpers;

namespace API.Activities.Validators
{
    public class ActivityViewModelValidator : AbstractValidator<ActivityApiModel>
    {
        public ActivityViewModelValidator()
        {
            this.RuleFor(m => m.Title.Length).GreaterThan(3)
                .WithMessage(ActivitiesErrorMessages.ActivityTitleLength);

            var lastCategoryTypeId = (int)Enum.GetValues<CategoryType>().MaxBy(c => (int) c);
            this.RuleFor(m => m.CategoryId)
                .GreaterThan(0)
                .LessThan(lastCategoryTypeId + 1)
                .WithMessage(ActivitiesErrorMessages.InvalidCategory);

            this.RuleFor(m => m.City).NotEmpty();
            this.RuleFor(m => m.Description).NotEmpty();
            this.RuleFor(m => m.Venue).NotEmpty();
            this.RuleFor(m => m.Date).GreaterThan(DateTime.MinValue);
        }
    }
}
