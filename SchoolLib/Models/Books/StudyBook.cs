using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    [DisplayName("Підручник")]
    public class StudyBook : Book
    {
        [Required(ErrorMessage = "Необхідно надати номер класу")]
        [Display(Name = "Клас")]
        [Range(1, 12, ErrorMessage = "Клас має можливий діапазон від {1} до {2}")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Необхідно надати назву предмету")]
        [Display(Name = "Предмет")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Предмет може мати від 4 до 20 символів")]
        public string Subject { get; set; }
    }
}
