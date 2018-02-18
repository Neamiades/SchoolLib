using System.Threading.Tasks;

namespace SchoolLib.Services
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}
}