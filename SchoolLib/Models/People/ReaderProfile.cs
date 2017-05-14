using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    public class ReaderProfile
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(10, ErrorMessage = "Имя должно содержать меньше 10 символов")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(5), MaxLength(15, ErrorMessage = "Фамилия должна содержать меньше 15 символов")]
        public string SurName { get; set; }

        [Required]
        [MinLength(5), MaxLength(15, ErrorMessage = "Отчество должно содержать меньше 15 символов")]
        public string Patronimic { get; set; }

        [Required]
        [MinLength(5), MaxLength(15, ErrorMessage = "Название улицы должно содержать меньше 15 символов")]
        public string Street { get; set; }

        [Required]
        public short Apartment { get; set; }

        [Required]
        public short House { get; set; }

        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
    }
}
