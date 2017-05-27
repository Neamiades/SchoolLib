using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    [DisplayName("Робітник")]
    public class Worker : Reader
    {
        [Required]
        [DisplayName("Посада")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Посада робітника повинна мати від 6 до 50 символів")]
        public string Position { get; set; }
    }
}
