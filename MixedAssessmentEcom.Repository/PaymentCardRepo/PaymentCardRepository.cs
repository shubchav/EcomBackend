using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.PaymentCardRepo
{
    public class PaymentCardRepository : IPaymentCardRepository
    {
        private readonly UserContext _context;
        public PaymentCardRepository(UserContext context)
        {
            _context = context;
        }

        public List<PaymentCard> GetAllCard()
        {
            return _context.PaymentCards.ToList();
        }

        public PaymentCard GetCard(int cardId)
        {
            var data = _context.PaymentCards.FirstOrDefault(d => d.CardId == cardId);
            return data;
        }

        public PaymentCard AddCard(PaymentCard card)
        {
            _context.PaymentCards.Add(card);
            _context.SaveChanges();
            return card;
        }

        public PaymentCard UpdateCard(PaymentCard oldData, PaymentCard newData)
        {
            _context.Entry<PaymentCard>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }


        public void DeleteCard(PaymentCard card)
        {
            _context.PaymentCards.Remove(card);
            _context.SaveChanges();
        }


        public PaymentCard CheckValidCard(PaymentCard card)
        {
            var data = _context.PaymentCards.Where(s => s.CardNumber == card.CardNumber && s.ExpiryDate == card.ExpiryDate && s.CVV == card.CVV).FirstOrDefault();
            return data;
        }



    }
}
