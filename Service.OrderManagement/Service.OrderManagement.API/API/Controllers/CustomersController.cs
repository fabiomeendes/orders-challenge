using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OrderManagement.API.Application.Services;

namespace Service.Customer.API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}/orders-count")]
        public async Task<IActionResult> GetOrderCount(int customerId)
        {
            var count = await _customerService.GetOrderCountForCustomer(customerId);
            return Ok(count);
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetOrdersForCustomer(int customerId)
        {
            var orders = await _customerService.GetOrdersForCustomer(customerId);
            return Ok(orders);
        }
    }
}
