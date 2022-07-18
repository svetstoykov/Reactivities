using System;
using API.Activities.Models;
using FluentValidation;
using Models.ErrorHandling.Helpers;

namespace API.Activities.Validators
{
    public class ActivityViewModelValidator : AbstractValidator<ActivityViewModel>
    {
        public ActivityViewModelValidator()
        {
            RuleFor(m => m.Title).NotEmpty();
            RuleFor(m => m.Category).NotEmpty();
            RuleFor(m => m.City).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
            RuleFor(m => m.Venue).NotEmpty();

            RuleFor(m => m.Date).Custom((dateString, context) =>
            {
                if (DateTime.TryParse(dateString, out var date))
                {
                    if (date < DateTime.MinValue)
                    {
                        context.AddFailure(ActivitiesErrorMessages.InvalidDate);
                    }

                    return;
                }

                context.AddFailure(ActivitiesErrorMessages.InvalidDate);
            });
        }
    }
}
