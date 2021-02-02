using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace ExadelBonusPlus.Services.Models
{
    public class BonusDtoValidator : AbstractValidator<AddBonusDto>
    {
		public BonusDtoValidator()
        {
            RuleFor(x => x.Name).Length(1, 9999);
            RuleFor(x => x.DateStart).NotNull();
            RuleFor(x => x.DateEnd).NotNull();
        }
	}
}
