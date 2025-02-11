namespace Refactoring.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using System.Security.Cryptography;
    using Microsoft.Data.Sqlite;
	using System.Data.Common;
	using Refactoring.Enums;
	using Refactoring.Services;
	using Refactoring.Repositories;

	[ApiController]
    [Route("/api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IProductRepository _productRepository;

        public OrderController(IOrderProcessingService orderProcessingService, IProductRepository productRepository)
        {
           _orderProcessingService = orderProcessingService;
           _productRepository = productRepository;
        }

        [HttpPost("process", Name = "ProcessOrder")]
        public async Task<IActionResult> ProcessOrder([FromQuery] int productId, [FromQuery] string productType, [FromQuery] PaymentType paymentType)
        {
            var product = _productRepository.GetProduct(productId, productType);
            
            if (product == null)
            {
                return BadRequest($"Product with id {productId} and type {productType} not found");
            }

            await _orderProcessingService.ProcessOrdersAsync(product, paymentType);
            return Ok();
        }
    }
}
