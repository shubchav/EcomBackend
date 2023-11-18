using AutoMapper;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.PaymentCardRepo;
using MixedAssessmentEcom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Servise.PaymentCardServ
{
    public class PaymentCardService : IPaymentCardService
    {
        private readonly IPaymentCardRepository _paymentRepo;
        private readonly IMapper _mapper;
        public PaymentCardService(IPaymentCardRepository paymentRepo, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }


        public List<PaymentCard> GetAllCard()
        {
            var pol = _paymentRepo.GetAllCard();

            return pol;
        }

        public PaymentCard GetPaymentCard(int cardId)
        {
            var card = _paymentRepo.GetCard(cardId);


            return card;
        }

        public PaymentCard AddCard(PaymentCard card)

        {

            var data = _paymentRepo.AddCard(card);
            return data;
        }

        public PaymentCard UpdateCard(int cardId, PaymentCard card)
        {

            var exitdata = _paymentRepo.GetCard(cardId);

            var productClass = new PaymentCard()
            {
                CardId = exitdata.CardId,
                CardNumber = card.CardNumber,
                ExpiryDate = card.ExpiryDate,
                CVV = card.CVV,
            };

            return productClass;
        }

        public bool DeleteCard(int cardId)
        {
            var del = _paymentRepo.GetCard(cardId);

            _paymentRepo.DeleteCard(del);
            return true;
        }

        public string PaymentCheck(PaymentCardVM paymentCartVM)
        {
            var paymentVM = new PaymentCardVM()
            {
                CardNumber = paymentCartVM.CardNumber,
                CVV = paymentCartVM.CVV,
                ExpiryDate = paymentCartVM.ExpiryDate,
            };

            var cardClass = _mapper.Map<PaymentCard>(paymentVM);

            var verify = _paymentRepo.CheckValidCard(cardClass);
            if (verify != null)
            {
                return "Payment Successfully Done.";
            }
            else
            {
                return "Invalid Card.";
            }


            
        }
    }
}
