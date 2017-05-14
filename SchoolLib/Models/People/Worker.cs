using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    public class Worker : Reader
    {
        [Required]
        [MinLength(6, ErrorMessage = "Должность работника должна быть не меньше 6-ти символов")]
        [MaxLength(50, ErrorMessage = "Должность работника должна быть не больше 50-ти символов")]
        public string Position { get; set; }
    }
}
