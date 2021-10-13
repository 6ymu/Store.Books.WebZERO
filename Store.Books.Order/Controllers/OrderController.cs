using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Books.Order.Interfaces;
using System.Threading.Tasks;

namespace Store.Books.Order.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IStoreService _service;
        public OrderController(IStoreService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Model.DTO.CreateOrder order)
        {
            var dbOrder = await _service.CreateOrUpdateOrder(order.BookId, order.Title, order.PriceId, order.Amount, order.UserName);
            return Ok(dbOrder);
        }
    }
}
