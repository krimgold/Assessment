using Refactoring.Dto;

namespace Refactoring.Processors.Payment
{
	public interface IPaymentProcessor
	{
		void ProcessPayment(Product product);
	}
}
