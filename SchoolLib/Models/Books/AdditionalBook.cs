using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    public class AdditionalBook : Book
    {
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Мова може мати від 3 до 15 символів")]
        public string Language { get; set; }

        [Required, StringLength(20, MinimumLength = 2, ErrorMessage = "Жанр книги може мати від 2 до 20 символів")]
        public string Cipher { get; set; }
    }
}
