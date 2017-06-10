using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.ManageViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [StringLength(13, ErrorMessage = "Некоректний формат номеру")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
