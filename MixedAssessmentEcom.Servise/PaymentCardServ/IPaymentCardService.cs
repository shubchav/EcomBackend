using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.ViewModels;

namespace MixedAssessmentEcom.Servise.PaymentCardServ
{
    public interface IPaymentCardService
    {
        PaymentCard AddCard(PaymentCard card);
        bool DeleteCard(int cardId);
        List<PaymentCard> GetAllCard();
        PaymentCard GetPaymentCard(int cardId);
        PaymentCard UpdateCard(int cardId, PaymentCard card);
        string PaymentCheck(PaymentCardVM paymentCartVM);
    }
}