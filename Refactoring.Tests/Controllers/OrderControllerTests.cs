using Microsoft.AspNetCore.Mvc;
using Moq;
using Refactoring.Controllers;
using Refactoring.Dto;
using Refactoring.Enums;
using Refactoring.Repositories;
using Refactoring.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring.Tests.Controllers
{
	[TestClass]
	public class OrderControllerTests
	{
		private readonly Mock<IProductRepository> _productRepositoryMock;
		private readonly Mock<IOrderProcessingService> _orderProcessingServiceMock;
		private readonly OrderController _sut;

        public OrderControllerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
			_orderProcessingServiceMock = new Mock<IOrderProcessingService>();
			_sut = new OrderController(_orderProcessingServiceMock.Object, _productRepositoryMock.Object);
        }

        [TestMethod]
		public async Task GivenAProductExists_WhenProcessOrderIsCalled_ThenSuccessIsReturned()
		{
			var productId = 1;
			var productType = "type1";
			var paymentType = PaymentType.PayPal;
			var product = new Product
			{
				Id = productId,
				Name = "Test",
				Price = 11.6
			};

			_productRepositoryMock.Setup(x => x.GetProduct(productId, productType)).Returns(product);
			
			var result = await _sut.ProcessOrder(productId, productType, paymentType);

			Assert.AreEqual(typeof(OkResult), result.GetType());
			_orderProcessingServiceMock.Verify(x => x.ProcessOrdersAsync(product, paymentType), Times.Once());
		}

		[TestMethod]
		public async Task GivenAProductIsNoFound_WhenProcessOrderIsCalled_ThenBadRequestIsReturned()
		{
			var productId = 1;
			var productType = "type1";
			var paymentType = PaymentType.PayPal;
			Product? product = null;

			_productRepositoryMock.Setup(x => x.GetProduct(productId, productType)).Returns(product);

			var result = await _sut.ProcessOrder(productId, productType, paymentType);

			Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
			Assert.AreEqual(((BadRequestObjectResult)result).Value, $"Product with id {productId} and type {productType} not found");
			_orderProcessingServiceMock.Verify(x => x.ProcessOrdersAsync(product, paymentType), Times.Never());
		}
	}
}
