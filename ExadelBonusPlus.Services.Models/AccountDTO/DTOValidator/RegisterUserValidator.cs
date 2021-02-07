using ExadelBonusPlus.WebApi.ViewModel;
using FluentValidation;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExadelBonusPlus.Services.Models.ViewModel
{
    class RegisterUserValidator : AbstractValidator<RegisterUserDTO> 
    {
        public RegisterUserValidator()
        {
            RuleFor(model => model.Email).NotNull().EmailAddress();
            RuleFor(model => model.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter your password")
                .Length(8, 25).WithMessage("Password's lenght must greater than 8 letters and less than 25 letters")
                .Must(IsValidPassword).WithMessage("Password's must contain alphabet, numeric, symbol");
            RuleFor(m => m.ConfirmPassword).Must((fooArgs, password) =>
                    IsPasswordConfirmed(fooArgs.ConfirmPassword, password))
                .WithMessage("Confirm password, please");
            RuleFor(model => model.LastName).NotNull().WithMessage("Please ensure that you have entered your Last Name"); ;
            RuleFor(model => model.FirstName).NotNull().WithMessage("Please ensure that you have entered your First Name"); ;
            RuleFor(model => model.City).NotNull();
            RuleFor(model => model.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter your password").
                Must(IsValidPhone).WithMessage("Password's must contain alphabet, numeric, symbol");
        }
        private bool IsPasswordConfirmed(string confirmPassword, string password)
        {
            return password.Equals(confirmPassword);
        }
        private bool IsValidPhone(string arg)
        {
            return Regex.IsMatch("phone number 123456789", "[0-9]{7,}");
        }
        private bool IsValidPassword(string arg)
        {
            if (arg.Any(s => char.IsSymbol(s)))
            {
                if (arg.Any(s => char.IsNumber(s)))
                {
                    if (arg.Any(s => char.IsLetter(s)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
