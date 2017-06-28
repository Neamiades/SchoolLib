using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    [DisplayName("Книга")]
    public class AdditionalBook : Book
    {
        [Required(ErrorMessage = "Необхідно надати мову книги")]
        [Display(Name = "Мова")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Мова може мати від 3 до 15 символів")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Необхідно надати шифр книги")]
        [Display(Name = "Шифр")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Шифр книги може мати від 2 до 20 символів")]
        public string Cipher { get; set; }
    }
}
