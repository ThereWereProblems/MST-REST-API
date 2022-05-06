using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MST_REST_Web_API.Models.DTO;
using MST_REST_Web_API.Services;

namespace MST_REST_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _ordersSrevice;

        public OrderController(IOrderService OrdersService)
        {
            _ordersSrevice = OrdersService;

        }
        [HttpPost("create")]
        [AllowAnonymous]
        public ActionResult Create([FromBody] CreateOrderDto dto)
        {


            var id = _ordersSrevice.Create(dto);

            return Created($"/api/orders/{id}", null);
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAllOrders()
        {
            var listOfOrders = _ordersSrevice.GetAll();

            return Ok(listOfOrders);
        }
    }
}
