using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolLib.Data.Validators
{
    public class YearRangeValidator : ValidationAttribute
    {
        int _lowYear;
        IFormatProvider culture = new CultureInfo("uk-UA");
        public new string ErrorMessage { get; set; }
        public YearRangeValidator(int lowYear)
        {

            if (lowYear <= 0)
                _lowYear = DateTime.Today.AddYears(lowYear).Year;
            else
                _lowYear = lowYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int year;
            var errMsg = $"Значення має бути між {_lowYear} та {DateTime.Now.Year} у форматі РРРР";
            if (!Int32.TryParse(value.ToString(), out year) || year > DateTime.Now.Year || year < _lowYear)
                return new ValidationResult(errMsg);

            return ValidationResult.Success;
        }
    
    }
}
