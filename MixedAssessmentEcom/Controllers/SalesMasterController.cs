using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Servise.SalesMasterServ;
using MixedAssessmentEcom.ViewModels.OrderHistoryVM;
using MixedAssessmentEcom.ViewModels.SalesMasterViewModels;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesMasterController : ControllerBase
    {
        private readonly ISalesMasterService _salesMServ ;
        public SalesMasterController(ISalesMasterService salesMServ)
        {
            _salesMServ = salesMServ;
        }



        //[Authorize]
        [HttpGet]
        public IActionResult GetAllSaleMaster()
        {
            var datas = _salesMServ.GetAllSalesMaster();
            return Ok(datas);
        }
        //[Authorize]
        [HttpGet("{userId}")]
        public IActionResult GetSalesMaster(int userId)
        {
            if (userId == 0 || userId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _salesMServ.GetSalesMaster(userId);
            return Ok(data);
        }
        [HttpGet("GetSalesMasterByInvoice/{invoiceId}")]
        public IActionResult GetSalesMasterByInvoice(string invoiceId)
        {
            if (invoiceId == null || invoiceId == "")
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _salesMServ.GetSalesMasterByInvoiceID(invoiceId);
            return Ok(data);
        }
        //[Authorize]
        [HttpPost]
        public IActionResult AddSalesMaster([FromBody] SalesMasterVM salesMVm)
        {
            if (salesMVm == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _salesMServ.AddSalesMaster(salesMVm);
            return Ok(adddata);
        }

        [HttpPut]
        public IActionResult UpdateSalesMaster(int userId, [FromBody] SalesMasterVM salesMaster)
        {
            if (userId == 0 || userId == null)
            {
                return NotFound("Id not found.");
            }
            var salesUp = (_salesMServ.UpdateSalesMaster(userId, salesMaster));
            return Ok(salesUp);
        }

        //[Authorize]
        [HttpDelete]
        public IActionResult DeleteSalesMaster(int salemasterId)
        {
            _salesMServ.DeleteSalesM(salemasterId);
            return Ok();
        }
        /// Order Details Table Code
        //[Authorize]
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrderHistory(int orderId)
        {
            _salesMServ.DeleteOrderHistory(orderId);
            return Ok();
        }

        [HttpPost("AddOrderHistory")]
        public IActionResult AddOrderHistory([FromBody] OrderHistoryUserIdVM orderVM)
        {
            if (orderVM == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _salesMServ.AddOrderHistory(orderVM);
            return Ok(adddata);
        }
        //by invoice Id

        [HttpGet("GetOrderByInvoice/{invoiceId}")]
        public IActionResult GetOrderByInvoice(string invoiceId)
        {
            if (invoiceId == null || invoiceId == "" )
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _salesMServ.GetAllOrderHstory(invoiceId);
            return Ok(data);
        }
        // by userid 
        [HttpGet("GetAllOrderByUserId/{userId}")]   
        public IActionResult GetAllOrderByUserId(int userId)
        {
            if (userId == null || userId == 0)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _salesMServ.GetAllOrderHstoryByUserId(userId);
            return Ok(data);
        }
    }
}
