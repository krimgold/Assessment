using Refactoring.Enums;

namespace Refactoring.Processors.Payment
{
	public class PaymentProcessingFactory : IPaymentProcessingFactory
	{
		public IPaymentProcessor GetProcessor(PaymentType paymentType)
		{
			switch(paymentType)
			{
				case PaymentType.CreditCard:
					return new CreditCardPaymentProcessor();
				case PaymentType.PayPal:
					return new PayPalPaymentProcessor();
				default:
					throw new Exception("Unsupported payment type");
			}
		}
	}
}
