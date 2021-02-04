using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelBonusPlus.Services.Models
{
    public class ValidationException: Exception
    {
        public List<String> ValidationErrors { get; set; }

        public ValidationException(List<String> validationErrors)
        {
            this.ValidationErrors = validationErrors;
        }

    }
}
