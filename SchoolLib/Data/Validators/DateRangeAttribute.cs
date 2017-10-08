using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SchoolLib.Data.Validators
{
    public class DateRangeAttribute : ValidationAttribute
    {
        DateTime _lowDate;
        readonly IFormatProvider _culture = new CultureInfo("uk-UA");
        public new string ErrorMessage { get; set; }
        public DateRangeAttribute(string lowDate)
        {
            _lowDate = String.IsNullOrWhiteSpace(lowDate)     ? DateTime.Today.AddYears(-5)
                     : lowDate == "now"                       ? DateTime.Today
                     : Int32.TryParse(lowDate, out var years) ? DateTime.Today.AddYears(years)
                                                              : DateTime.ParseExact(lowDate, "dd.MM.yyyy", _culture);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value as string != "system-auto")
            {
                var errMsg = $"Значення має бути між {_lowDate.Date:dd.MM.yyyy} та {DateTime.Now:dd.MM.yyyy} у форматі дд.мм.рррр";

                if (!DateTime.TryParse(value.ToString(), _culture, DateTimeStyles.AssumeLocal, out var date) ||
                    date > DateTime.Now || date < _lowDate)
                {
                    return new ValidationResult(errMsg);
                }
            }
            
            return ValidationResult.Success;
        }

    }
}
