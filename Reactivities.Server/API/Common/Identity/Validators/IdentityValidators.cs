using System.Text.RegularExpressions;
using API.Common.Identity.Models;
using Application.Common.ErrorHandling;
using FluentValidation;

namespace API.Common.Identity.Validators
{
    public class IdentityValidators
    {
        public class LoginApiModelValidator : AbstractValidator<LoginApiModel>
        {
            public LoginApiModelValidator()
            {
                this.RuleFor(m => m.Email).NotEmpty();
                this.RuleFor(m => m.Password).NotEmpty();
            }
        }

        public class RegisterApiModelValidator : AbstractValidator<RegisterApiModel>
        {
            public RegisterApiModelValidator()
            {
                this.RuleFor(m => m.Email).NotEmpty();

                // 8-40 chars, 1 lower, 1 upper, 1 spec symbol, 1 number
                this.RuleFor(m => m.Password)
                    .Matches(new Regex(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40})"))
                    .WithMessage(CommonErrorMessages.PasswordRequirementsNotMet);
            }
        }
    }
}
