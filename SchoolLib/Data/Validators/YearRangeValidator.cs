using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Data.Validators
{
    public class YearRangeValidator : ValidationAttribute
    {
        readonly int _lowYear;
        public new string ErrorMessage { get; set; }
        public YearRangeValidator(int lowYear)
        {
            _lowYear = lowYear <= 0 ? DateTime.Today.AddYears(lowYear).Year : lowYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errMsg = $"Значення має бути між {_lowYear} та {DateTime.Now.Year} у форматі РРРР";

            if (!Int32.TryParse(value.ToString(), out var year) || year > DateTime.Now.Year || year < _lowYear)
                return new ValidationResult(errMsg);

            return ValidationResult.Success;
        }
    
    }
}
