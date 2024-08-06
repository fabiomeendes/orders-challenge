using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.OrderManagement.API.Application.Services;

namespace Service.Customer.API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}/total-value")]
        public async Task<IActionResult> GetTotalValue(int orderId)
        {
            var totalValue = await _orderService.GetTotalValue(orderId);
            return Ok(totalValue);
        }
    }
}
