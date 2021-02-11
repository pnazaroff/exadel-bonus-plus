using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;

namespace ExadelBonusPlus.Services.Models.DTOValidator
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(model => model.Id)
                .NotNull().WithMessage("Enter User Id");
            RuleFor(model => model.LastName)
                .NotNull().WithMessage("Please enter your Lastname");
            RuleFor(model => model.FirstName)
                .NotNull().WithMessage("Please enter your Firstname");
            RuleFor(model => model.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Please enter your phonenumber").
                Must(IsValidPhone).WithMessage("Please check your phonenumber");
            RuleFor(model => model.City)
                .NotNull().WithMessage("Enter your city");
        }
        private bool IsValidPhone(string arg)
        {
            return Regex.IsMatch("phone number 123456789", "[0-9]{7,}");
        }
    }
}
