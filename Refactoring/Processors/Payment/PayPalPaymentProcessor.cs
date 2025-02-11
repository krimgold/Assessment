using Refactoring.Dto;

namespace Refactoring.Processors.Payment
{
	public class PayPalPaymentProcessor : IPaymentProcessor
	{
		public void ProcessPayment(Product product)
		{
			Console.WriteLine($"Processing PayPal payment for {product.Price}");
		}
	}
}
