using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    public class ExternalBook : Book
    {
        [Required, StringLength(20, MinimumLength = 5, ErrorMessage = "Жанр книги може мати від 5 до 20 символів")]
        public string Genre { get; set; }
    }
}
