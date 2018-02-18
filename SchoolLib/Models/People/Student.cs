using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
	[DisplayName("Учень")]
	public class Student : Reader
	{
		[Required(ErrorMessage = "Необхідно надати номер класу")]
		[RegularExpression(@"[1-9][0-2]?-\w", ErrorMessage = "Назва класу повинна мати формат Ч[Ч]-Б (Ч - число, Б - буква)")]
		[Display(Name = "Клас")]
		public string Grade { get; set; }
	}
}