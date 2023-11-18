using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Servise.ProductServ;
using MixedAssessmentEcom.ViewModels.ProductViewModel;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServise _productSer;
        public ProductController(IProductServise productSer)
        {
            _productSer = productSer;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var datas = _productSer.GetProducts();
            return Ok(datas);
        }
        //[Authorize]
        [HttpGet("{productId}")]
        public IActionResult GetProduct(int productId)
        {
            if (productId == 0 || productId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _productSer.GetProduct(productId);
            return Ok(data);
        }
        //[Authorize]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductVM productVM)
        {
            if (productVM == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _productSer.AddProduct(productVM);
            return Ok(adddata);
        }
        //[Authorize]
        [HttpPut]
        public IActionResult UpdateProduct(int productId, [FromBody] Product productVm)
        {
            if (productId == 0 || productId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_productSer.UpdateProduct(productId, productVm));
            return Ok(produtUp);
        }
        //[Authorize]
        [HttpDelete]
        public IActionResult DeleteProduct(int productId)
        {
            _productSer.DeleteProduct(productId);
            return Ok();
        }
        // discount table part start ///////////////////////////////////////////////////////////////////////

        //[Authorize]
        [HttpPost("AddDiscount")]
        public IActionResult AddDiscount([FromBody] Discount discount)
        {
            if (discount == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _productSer.AddDiscount(discount);
            return Ok(adddata);
        }
        //[Authorize]
        [HttpGet("GetAllDiscount")]
        public IActionResult GetAllDiscount()
        {
            var datas = _productSer.GetAllDiscount();
            return Ok(datas);
        }
        //[Authorize]
        [HttpGet("GetDiscount/{dicountId}")]
        public IActionResult GetDiscount(int dicountId)
        {
            if (dicountId == 0 || dicountId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _productSer.GetDiscount(dicountId);
            return Ok(data);
        }
      
      
        //[Authorize]
        [HttpPut("UpdateDiscount")]
        public IActionResult UpdateDiscount(int dicountId, [FromBody] Discount discount)
        {
            if (dicountId == 0 || dicountId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_productSer.UpdateDiscount(dicountId, discount));
            return Ok(produtUp);
        }
        //[Authorize]
        [HttpDelete("DeleteDiscount")]
        public IActionResult DeleteDiscount(int dicountId)
        {
            _productSer.DeleteDiscount  (dicountId);
            return Ok();
        }
    }
}
