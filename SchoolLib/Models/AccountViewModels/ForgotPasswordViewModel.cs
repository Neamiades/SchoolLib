using System.ComponentModel.DataAnnotations;

namespace SchoolLib.Models.AccountViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}