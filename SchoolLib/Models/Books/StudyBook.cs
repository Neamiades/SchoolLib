using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.Books
{
    public class StudyBook : Book
    {
        [Required]
        public int Grade { get; set; }

        [Required, StringLength(20, MinimumLength = 4)]
        public string Subject { get; set; }
    }
}
