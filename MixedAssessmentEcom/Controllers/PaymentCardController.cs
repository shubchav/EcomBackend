using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Servise.PaymentCardServ;
using MixedAssessmentEcom.ViewModels;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCardController : ControllerBase
    {
        private readonly IPaymentCardService _paymentService;
        public PaymentCardController(IPaymentCardService paymentService)
        {
            _paymentService = paymentService;
        }
        //[Authorize]
        [HttpGet]
        public IActionResult GetAllCards()
        {
            var datas = _paymentService.GetAllCard();
            return Ok(datas);
        }
        //[Authorize]
        [HttpGet("{cardId}")]
        public IActionResult GetCard(int cardId)
        {
            if (cardId == 0 || cardId == null)
            {
                return BadRequest("Id  0 or null Not Accepted ");
            }
            var data = _paymentService.GetPaymentCard(cardId);
            return Ok(data);
        }
        //[Authorize]
        [HttpPost]
        public IActionResult AddCard([FromBody] PaymentCard card)
        {
            if (card == null)
            {
                return BadRequest("Please fill the data.");
            }
            var adddata = _paymentService.AddCard(card);
            return Ok(adddata);
        }
        //[Authorize]
        [HttpPut]
        public IActionResult UpdateProduct(int cardId, [FromBody] PaymentCard card)
        {
            if (cardId == 0 || cardId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_paymentService.UpdateCard(cardId, card));
            return Ok(produtUp);
        }
        //[Authorize]
        [HttpDelete]
        public IActionResult DeleteCard(int cardId)
        {
            _paymentService.DeleteCard(cardId);
            return Ok();
        }

        [HttpPost("verifyCard")]
        public IActionResult VerifyCardDetails([FromBody] PaymentCardVM paymentCardVM)
        {
            if(paymentCardVM.CardNumber == null || paymentCardVM.CVV == null
                || paymentCardVM.ExpiryDate == null || paymentCardVM.CardNumber == "")
            {
                return BadRequest("All Field are Mandetory.");
            }


            var response = _paymentService.PaymentCheck(paymentCardVM);
            return Ok(response);

        }

    }
}
