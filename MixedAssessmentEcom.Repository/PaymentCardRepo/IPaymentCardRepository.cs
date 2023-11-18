using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.PaymentCardRepo
{
    public interface IPaymentCardRepository
    {
        PaymentCard AddCard(PaymentCard card);
        void DeleteCard(PaymentCard card);
        List<PaymentCard> GetAllCard();
        PaymentCard GetCard(int cardId);
        PaymentCard UpdateCard(PaymentCard oldData, PaymentCard newData);
        PaymentCard CheckValidCard(PaymentCard card);
    }
}