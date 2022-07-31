using System;
using System.Linq;
using API.Activities.Models;
using Application.Activities.ErrorHandling;
using FluentValidation;
using Models.Enumerations;

namespace API.Activities.Validators
{
    public class ActivityViewModelValidator : AbstractValidator<ActivityApiModel>
    {
        public ActivityViewModelValidator()
        {
            this.RuleFor(m => m).NotNull().NotEmpty()
                .DependentRules(() =>
                {
                    this.RuleFor(m => m.Title).NotNull()
                        .DependentRules(() =>
                        {
                            this.RuleFor(m => m.Title.Length).GreaterThan(3)
                                .WithMessage(ActivitiesErrorMessages.ActivityTitleLength);
                        });

                    var lastCategoryTypeId = (int) Enum.GetValues<CategoryType>().MaxBy(c => (int) c);
                    this.RuleFor(m => m.CategoryId)
                        .GreaterThan(0)
                        .WithMessage(ActivitiesErrorMessages.InvalidCategory)
                        .LessThan(lastCategoryTypeId + 1)
                        .WithMessage(ActivitiesErrorMessages.InvalidCategory);

                    this.RuleFor(m => m.City).NotEmpty();
                    this.RuleFor(m => m.Description).NotEmpty();
                    this.RuleFor(m => m.Venue).NotEmpty();
                    this.RuleFor(m => m.Date)
                        .GreaterThan(DateTime.MinValue)
                        .WithMessage(ActivitiesErrorMessages.InvalidDate);
                });
        }
    }
}
