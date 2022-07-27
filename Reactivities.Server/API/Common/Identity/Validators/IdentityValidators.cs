using System.Text.RegularExpressions;
using API.Common.Identity.Models;
using FluentValidation;
using Models.ErrorHandling.Helpers;

namespace API.Common.Identity.Validators
{
    public class IdentityValidators
    {
        public class LoginApiModelValidator : AbstractValidator<LoginApiModel>
        {
            public LoginApiModelValidator()
            {
                RuleFor(m => m.Email).NotEmpty();
                RuleFor(m => m.Password).NotEmpty();
            }
        }

        public class RegisterApiModelValidator : AbstractValidator<RegisterApiModel>
        {
            public RegisterApiModelValidator()
            {
                RuleFor(m => m.Email).NotEmpty();

                // 8-40 chars, 1 lower, 1 upper, 1 spec symbol, 1 number
                RuleFor(m => m.Password)
                    .Matches(new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40})"))
                    .WithMessage(IdentityErrorMessages.PasswordRequirementsNotMet);
            }
        }
    }
}
