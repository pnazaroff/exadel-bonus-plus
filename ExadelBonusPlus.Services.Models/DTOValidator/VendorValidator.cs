using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models.DTOValidator
{
    public class AddVendorDtoValidator : AbstractValidator<AddVendorDto>
    {
        public AddVendorDtoValidator()
        {
            RuleFor(v => v).NotNull();
            RuleFor(v => v.Name).NotEmpty().NotEmpty();
            RuleFor(v => v.Email).NotNull().NotEmpty().EmailAddress(); ;
        }
    }

    public class VendorDtoValidator:AbstractValidator<VendorDto>
    {
        
        public VendorDtoValidator()
        {
            RuleFor(v => v).NotNull();
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.Name).Length(1, int.MaxValue);
            RuleFor(v => v.Email).NotNull().EmailAddress();
        }
    }
}
