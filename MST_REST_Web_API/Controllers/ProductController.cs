using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MST_REST_Web_API.Entities;
using MST_REST_Web_API.Services;

namespace MST_REST_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addproduct")]
        [Authorize(Roles = "Shopkeeper")]
        public ActionResult AddProduct([FromBody] Product dto)
        {
            _productService.AddProduct(dto);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Shopkeeper")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _productService.DeleteProduct(id);

            return NoContent();
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Shopkeeper")]
        public ActionResult UpdateProduct([FromRoute] int id, [FromBody] Product dto)
        {
            _productService.UpdateProduct(id, dto);

            return NoContent();
        }

        [HttpGet("getall")]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            var list = _productService.GetAll();

            return Ok(list);
        }
    }
}
