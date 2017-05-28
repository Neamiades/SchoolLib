using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    [DisplayName("Робітник")]
    public class Worker : Reader
    {
        [Required]
        [Display(Name = "Посада")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Посада робітника повинна мати від 4 до 50 символів")]
        public string Position { get; set; }
    }
}
