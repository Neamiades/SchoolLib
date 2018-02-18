using System.Threading.Tasks;

namespace SchoolLib.Services
{
	public interface ISmsSender
	{
		Task SendSmsAsync(string number, string message);
	}
}