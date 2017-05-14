using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    public class Student : Reader
    {
        [Required, MinLength(1), MaxLength(10)]
        public string Grade { get; set; }
    }
}
