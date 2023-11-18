using MixedAssessmentEcom.ViewModels.CartDetailsViewModels;

namespace MixedAssessmentEcom.Servise.CartDetailsServ
{
    public interface ICartDetailsService
    {
        CartDetailsVM AddCartDetail(CartDetailsVM cartDetailVM);
        bool DeleteCartDetail(int cartDetailId);
        List<CartDetailsVM> GetAllCartDetsils();
        CartDetailsVM GetCartDetail(int cartDetailId);
        CartDetailsVM UpdateCartDetail(int cartDetailId, CartDetailsVM cartDVM);
        List<CartDetailsVM> GetCartDetailByCartId(int cartId);
        CartDetailsVM GetCartDetailByUserId(int userId);
        bool DeleteCartDetailByProductId(int productId);
        List<CartDetailsVM> GetQuantityCartDetailByUserId(int userId);

        List<CartDetailsVM> GetAllRelatedCartDetailByUserId(int userId);
        string IncreaseUpdateQuantiy(int cartDetailId, CartDetailQtyUpdateVm cartDVM);   
        string DecreaseUpdateQuantiy(int cartDetailId, CartDetailQtyUpdateVm cartDVM);
        List<CartDetailsVM> GetQuantityCartDetailByMCartId(int mastrerCartId);
    }
}