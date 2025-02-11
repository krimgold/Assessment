using Refactoring.Dto;
using Refactoring.Enums;

namespace Refactoring.Services
{
	public interface IOrderProcessingService
	{
		Task ProcessOrdersAsync(Product product, PaymentType paymentType);
	}
}
