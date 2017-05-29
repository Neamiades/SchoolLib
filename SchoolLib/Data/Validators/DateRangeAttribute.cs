using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SchoolLib.Data.Validators
{
    public class DateRangeAttribute : ValidationAttribute
    {
        DateTime _lowDate;
        IFormatProvider culture = new CultureInfo("uk-UA");
        public new string ErrorMessage { get; set; }
        public DateRangeAttribute(string lowDate)
        {
            //DateTime date = DateTime.Parse(strDate, culture, DateTimeStyles.AssumeLocal);
            int years;

            if (String.IsNullOrWhiteSpace(lowDate))
                _lowDate = DateTime.Today.AddYears(-5);
            else if (lowDate == "now")
                _lowDate = DateTime.Today;
            else if (Int32.TryParse(lowDate, out years))
                _lowDate = DateTime.Today.AddYears(years);
            else
                _lowDate = DateTime.ParseExact(lowDate, "dd.mm.yyyy", culture);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            var errMsg = $"Значення має бути між {_lowDate.Date.ToString("dd.mm.yyyy")}" +
                $" та {DateTime.Now.ToString("dd.mm.yyyy")} у форматі дд.мм.рррр";
            if (!DateTime.TryParse(value.ToString(), culture, DateTimeStyles.AssumeLocal, out date) ||
                date > DateTime.Now || date < _lowDate) 
                return new ValidationResult(errMsg);
            
            return ValidationResult.Success;
        }

    }
}
