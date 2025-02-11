
namespace Refactoring.Services
{
	public class ConfirmationService : IConfirmationService
	{
		private readonly HttpClient _httpClient;
        public ConfirmationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task SendConfirmationAsync()
		{
			var response = await _httpClient.GetStringAsync("send?to=user@example.com&message=Order confirmed");
			Console.WriteLine(response);
		}
	}
}
