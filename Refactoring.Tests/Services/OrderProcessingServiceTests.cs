using Moq;
using NUnit.Framework.Internal;
using Refactoring.Dto;
using Refactoring.Enums;
using Refactoring.Processors.Payment;
using Refactoring.Repositories;
using Refactoring.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring.Tests.Services
{
	[TestClass]
	public class OrderProcessingServiceTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly Mock<IPaymentProcessingFactory> _paymentProcessingFactoryMock;
		private readonly Mock<IConfirmationService> _confirmationServiceMock;
		private readonly OrderProcessingService _sut;

        public OrderProcessingServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
			_paymentProcessingFactoryMock = new Mock<IPaymentProcessingFactory>();
			_confirmationServiceMock = new Mock<IConfirmationService>();
			_sut = new OrderProcessingService(_productRepositoryMock.Object, _paymentProcessingFactoryMock.Object, _confirmationServiceMock.Object);
		}


        [TestMethod]
		public async Task WhenProcessOrdersAsyncIsCalled_ThenDependenciesAreCalled()
		{
			var _paymentProcessorMock = new Mock<IPaymentProcessor>();
			_paymentProcessingFactoryMock.Setup(x => x.GetProcessor(PaymentType.PayPal)).Returns(_paymentProcessorMock.Object);

			var product = new Product
			{
				Id = 1,
				Name = "Test",
				Price = 11.6
			};
			await _sut.ProcessOrdersAsync(product, PaymentType.PayPal);

			_paymentProcessorMock.Verify(x => x.ProcessPayment(product), Times.Once);
			_confirmationServiceMock.Verify(x => x.SendConfirmationAsync(), Times.Once);
		}
	}
}
