using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Data.Validators
{
	public class YearRangeValidator : ValidationAttribute
	{
		private readonly int _lowYear;

		public YearRangeValidator(int lowYear)
		{
			_lowYear = lowYear <= 0 ? DateTime.Today.AddYears(lowYear).Year : lowYear;
		}

		public new string ErrorMessage { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var errMsg = $"Значення має бути між {_lowYear} та {DateTime.Now.Year} у форматі РРРР";

			if (!int.TryParse(value.ToString(), out var year) || year > DateTime.Now.Year || year < _lowYear)
				return new ValidationResult(errMsg);

			return ValidationResult.Success;
		}
	}
}