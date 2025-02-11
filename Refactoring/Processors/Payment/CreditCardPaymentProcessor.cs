

using Refactoring.Dto;

namespace Refactoring.Processors.Payment
{
	public class CreditCardPaymentProcessor : IPaymentProcessor
	{
		public void ProcessPayment(Product product)
		{
			Console.WriteLine($"Processing credit card payment for {product.Price}");
		}
	}
}
