using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Servise.CartDetailsServ;
using MixedAssessmentEcom.ViewModels.CartDetailsViewModels;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailsController : ControllerBase
    {
        private readonly ICartDetailsService _cartDService;
        public CartDetailsController(ICartDetailsService cartDService)
        {
            _cartDService = cartDService;
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAllCartDetsils()
        {
            var datas = _cartDService.GetAllCartDetsils;
            return Ok(datas);
        }
        //[Authorize]
        [HttpGet("{cartDetailId}")]
        public IActionResult GetCartDetail(int cartDetailId)
        {
            if (cartDetailId == 0 || cartDetailId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _cartDService.GetCartDetail(cartDetailId);
            return Ok(data);
        }
        // by cart id
        [HttpGet("GetCartDetailByCartId/{cartId}")]
        public IActionResult GetCartDetailByCartId(int cartId)
        {
            if (cartId == 0 || cartId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _cartDService.GetCartDetailByCartId(cartId);
            return Ok(data);
        }

        //by user id  
        [HttpGet("GetCartDetailByUserId/{userId}")]
        public IActionResult GetCartDetailByUserId(int userId)
        {
            if (userId == 0 || userId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _cartDService.GetCartDetailByUserId(userId);
            if (data == null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(data);
        }
        [HttpGet("GetAllRelatedCartDetailByUserId/{userId}")]
        public IActionResult GetAllRelatedCartDetailByUserId(int userId)
        {
            if (userId == 0 || userId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _cartDService.GetAllRelatedCartDetailByUserId(userId);
            if (data == null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(data);
        }

        [HttpGet("GetQuantityCartDetailByUserId/{userId}")]
        public IActionResult GetQuantityCartDetailByUserId(int userId)
        {
            if (userId == 0 || userId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _cartDService.GetQuantityCartDetailByUserId(userId);
            if (data == null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(data);
        }

        [HttpGet("GetQuantityCartDetailByMCartId/{masterCartId}")]
        public IActionResult GetQuantityCartDetailByMCartId(int masterCartId)
        {
            if (masterCartId == 0 || masterCartId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _cartDService.GetQuantityCartDetailByMCartId(masterCartId);
            if (data == null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(data);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult AddCartDetail([FromBody] CartDetailsVM cartDVM)
        {
            if (cartDVM == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _cartDService.AddCartDetail(cartDVM);
            return Ok(adddata);
        }

        // for add qty
        //[Authorize]
        [HttpPut("IncreaseUpdateQty")]
        public IActionResult IncreaseUpdateQty(int cartDId, [FromBody] CartDetailQtyUpdateVm cartDVm)
        {
            if (cartDId == 0 || cartDId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_cartDService.IncreaseUpdateQuantiy(cartDId, cartDVm));
            return Ok(produtUp);
        }


        // for subtract qty
        //[Authorize]
        [HttpPut("SubtractUpdateQty")]
        public IActionResult SubtractUpdateQty(int cartDId, [FromBody] CartDetailQtyUpdateVm cartDVm)
        {
            if (cartDId == 0 || cartDId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_cartDService.DecreaseUpdateQuantiy(cartDId, cartDVm));
            return Ok(produtUp);
        }


        //[Authorize]
        [HttpDelete("DeleteProduct/{cartDId}")]
        public IActionResult DeleteProduct(int cartDId)
        {
            _cartDService.DeleteCartDetail(cartDId);
            return Ok();
        }
        [HttpDelete("productId")]
        public IActionResult DeleteProductByProductId(int productId)
        {
            _cartDService.DeleteCartDetailByProductId(productId);
            return Ok();
        }
    }
}
