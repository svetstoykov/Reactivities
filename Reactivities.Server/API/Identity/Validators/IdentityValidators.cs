using System.Text.RegularExpressions;
using API.Identity.Models;
using Application.Common.ErrorHandling;
using FluentValidation;

namespace API.Identity.Validators;

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
        private const string ValidPasswordPattern = @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40})";
        
        public RegisterApiModelValidator()
        {
            this.RuleFor(m => m.Email).NotEmpty();

            // 8-40 chars, 1 lower, 1 upper, 1 spec symbol, 1 number
            this.RuleFor(m => m.Password)
                .Matches(new Regex(ValidPasswordPattern))
                .WithMessage(CommonErrorMessages.PasswordRequirementsNotMet);
        }
    }
}