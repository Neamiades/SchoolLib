using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    public class AdditionalBook : Book
    {
        [Required, StringLength(15, MinimumLength = 3)]
        public string Language { get; set; }

        [Required, StringLength(20, MinimumLength = 5)]
        public string Genre { get; set; }
    }
}
