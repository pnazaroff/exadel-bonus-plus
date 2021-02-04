using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ExadelBonusPlus.Services.Models
{
    public class AddBonusDtoValidator : AbstractValidator<AddBonusDto>
    {
		public AddBonusDtoValidator()
        {
            RuleFor(x => x.Title).Length(1, 9999);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DateStart).NotNull();
            RuleFor(x => x.DateEnd).NotNull();
        }
	}

    public class BonusDtoValidator : AbstractValidator<BonusDto>
    {
        public BonusDtoValidator()
        {
            RuleFor(x => x.Title).Length(1, 9999);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DateStart).NotNull();
            RuleFor(x => x.DateEnd).NotNull();
        }
    }
}
