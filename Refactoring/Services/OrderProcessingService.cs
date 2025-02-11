using Refactoring.Dto;
using Refactoring.Enums;
using Refactoring.Processors.Payment;
using Refactoring.Repositories;

namespace Refactoring.Services
{
	public class OrderProcessingService : IOrderProcessingService
	{
        private readonly IProductRepository _productRepository;
		private readonly IPaymentProcessingFactory _paymentProcessingFactory;
		private readonly IConfirmationService _confirmationService;

        public OrderProcessingService(IProductRepository productRepository, IPaymentProcessingFactory paymentProcessingFactory, IConfirmationService confirmationService)
        {
            _productRepository = productRepository;
			_paymentProcessingFactory = paymentProcessingFactory;
			_confirmationService = confirmationService;

        }

		public async Task ProcessOrdersAsync(Product product, PaymentType paymentType)
		{
			var paymentProcessor = _paymentProcessingFactory.GetProcessor(paymentType);

			paymentProcessor.ProcessPayment(product);

			await _confirmationService.SendConfirmationAsync();
		}
	}
}
