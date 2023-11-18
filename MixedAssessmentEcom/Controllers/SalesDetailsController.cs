using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Servise.SalesDetailServ;
using MixedAssessmentEcom.ViewModels.SalesDetailViewModels;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDetailsController : ControllerBase
    {
        private readonly ISalesDetailService _salesDetailSer;
        public SalesDetailsController(ISalesDetailService salesDetailSer)
        {
            _salesDetailSer = salesDetailSer;
        }

        [HttpPost]
        public IActionResult AddSalesDetails(SalesDetailVM salesDetailVM)
        {
            if (salesDetailVM == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _salesDetailSer.AddSalesDetail(salesDetailVM);
            return Ok(adddata);
        }

        [HttpGet]
        public IActionResult GetSalesSetailbyInvoiceId(string invoiceId)
        {
            if (invoiceId == "" || invoiceId == null)
            {
                return BadRequest("Id  empty or null Not Accepted ");
            }
            var data = _salesDetailSer.GetSalesDetailByInvoiceId(invoiceId);
            return Ok(data);
        }
    }
}
