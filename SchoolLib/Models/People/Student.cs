using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    [DisplayName("Учень")]
    public class Student : Reader
    {
        [Required(ErrorMessage = "Необхідно надати номер класу")]
        [RegularExpression(@"[1-9][0-2]?-\w", ErrorMessage = "Некоректна назва класу")]
        [Display(Name = "Клас")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Назва класу учня повинна мати від 1 до 10 символів")]
        public string Grade { get; set; }
    }
}
