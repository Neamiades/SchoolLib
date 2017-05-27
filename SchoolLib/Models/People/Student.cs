using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    [DisplayName("Учень")]
    public class Student : Reader
    {
        [Required]
        [DisplayName("Клас")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Назва класу учня повинна мати від 1 до 10 символів")]
        public string Grade { get; set; }
    }
}
