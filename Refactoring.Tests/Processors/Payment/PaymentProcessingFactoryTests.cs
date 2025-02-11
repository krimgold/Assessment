using Refactoring.Enums;
using Refactoring.Processors.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring.Tests.Processors.Payment
{
	[TestClass]
	public class PaymentProcessingFactoryTests
	{
		[DataTestMethod]
		[DataRow(PaymentType.CreditCard, typeof(CreditCardPaymentProcessor))]
		[DataRow(PaymentType.PayPal, typeof(PayPalPaymentProcessor))]
		public void WhenGetProcessorIsCalled_CorrectProcessorIsReturned(PaymentType paymentType, Type processorType)
		{
			var factory = new PaymentProcessingFactory();
			var processor = factory.GetProcessor(paymentType);

			Assert.AreEqual(processorType, processor.GetType());
		}
	}
}
