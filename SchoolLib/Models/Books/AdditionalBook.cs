using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    [DisplayName("Книжка")]
    public class AdditionalBook : Book
    {
        [DisplayName("Мова")]
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Мова може мати від 3 до 15 символів")]
        public string Language { get; set; }

        [DisplayName("Шифр")]
        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Шифр книги може мати від 2 до 20 символів")]
        public string Cipher { get; set; }
    }
}
