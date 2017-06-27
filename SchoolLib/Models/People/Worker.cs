using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.People
{
    [DisplayName("Співробітник")]
    public class Worker : Reader
    {
        [Required(ErrorMessage = "Необхідно надати іменування посади")]
        [Display(Name = "Посада")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Посада робітника повинна мати від 4 до 50 символів")]
        public string Position { get; set; }
    }
}
