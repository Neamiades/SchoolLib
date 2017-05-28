using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    [DisplayName("Незареєстрована книга")]
    public class ExternalBook : Book
    {
        [Display(Name = "Жанр")]
        [Required, StringLength(20, MinimumLength = 5, ErrorMessage = "Жанр книги може мати від 5 до 20 символів")]
        public string Genre { get; set; }
    }
}
