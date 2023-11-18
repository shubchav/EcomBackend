using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Servise.MasterCartServ;
using MixedAssessmentEcom.ViewModels.CartMasterViewModels;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterCartController : ControllerBase
    {
        private readonly IMasterCartService _mCartSer;

        public MasterCartController(IMasterCartService mCartSer)
        {
            _mCartSer = mCartSer;
        }

        [HttpGet]
        public IActionResult GetAllMCartDetails()
        {
            var masterData = _mCartSer.GetAllMasterCart();
            if (masterData == null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(masterData);

        }

        [HttpGet("{userId}")]
        public IActionResult GetDataByUId(int userId)
        {
            if(userId == 0 || userId == null)
            {
                return BadRequest("Please insert valid id.");
            }
            var data = _mCartSer.GetMCartDByUId(userId);
            return Ok(data);
        }


        [HttpPost]
        public IActionResult AddNewCartMaster(MasterCartVM masterCartVM)
        {
            if (masterCartVM == null)
            {
               return BadRequest(" Can't add Please insert values.");

            }
           var cartdata =  _mCartSer.AddMasterCart(masterCartVM);
            return Ok(cartdata);
        }

        [HttpPut]
        public IActionResult UpdateCartM(int cartMId , MasterCartVM masterCartVM)
        {
            if(cartMId == 0 || cartMId == null || masterCartVM == null )
            {
                return BadRequest("Empty data can not be accepted.");
            }
            var data = _mCartSer.UpdateMasterCart(cartMId,masterCartVM);
            return(Ok(data));

            
        }
    }
}
