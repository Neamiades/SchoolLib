using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolLib.Data.Validators;

namespace SchoolLib.Models.People
{
	[DisplayName("Вибуття читача")]
	public class Drop
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Необхідно надати дату вибуття")]
		[Display(Name = "Дата")]
		[StringLength(10, ErrorMessage = "Некоректний формат дати")]
		[DateRange("now")]
		public string Date { get; set; }

		[Display(Name = "Причина")]
		[StringLength(30, MinimumLength = 5, ErrorMessage = "Опис причини може містити від 5 до 30 символів")]
		public string Couse { get; set; }

		[Display(Name = "Примітка")]
		[StringLength(250, ErrorMessage = "Опис не може містити більше 250 символів")]
		public string Note { get; set; }

		[Display(Name = "Ідентифікаційний № читача")]
		public int ReaderId { get; set; }

		public Reader Reader { get; set; }
	}
}