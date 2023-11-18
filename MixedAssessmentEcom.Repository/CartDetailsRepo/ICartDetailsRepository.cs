using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.CartDetailsRepo
{
    public interface ICartDetailsRepository
    {
        CartDetail AddCartDetails(CartDetail cart);
        void DeleteCartDetails(CartDetail cart);
        CartDetail GetCartDetail(int cartDetailId);
        List<CartDetail> GetCartDetails();
        CartDetail UpdateCartDetails(CartDetail oldData, CartDetail newData);
        List<CartDetail> GetCartDetailByMasterId(int cartId);
        CartDetail GetCartDetailByUserId(int userId);
        void DeleteCartDetailsByProductId(CartDetail cart);
        CartDetail GetCartDetailByProductId(int productId);
         List<CartDetail> GetAllRelatedCartDetailByUserId(int userId);
        List<CartDetail> GetAllQuantityCartDetailByUserId(int userId);
        CartDetail GetInvoiceLastIdByUserId(int userId);
        List<CartDetail> GetAllQuantityCartDetailByMCartId(int masterCartId);
        // new method for a adding a data 
        List<CartDetail> GetAllRelatedCartDetailByUserIdAndCartId(int userId, int mastercartId);
    }
}