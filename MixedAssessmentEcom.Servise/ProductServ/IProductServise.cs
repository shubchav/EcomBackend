using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.ViewModels.ProductViewModel;

namespace MixedAssessmentEcom.Servise.ProductServ
{
    public interface IProductServise
    {
        ProductVM AddProduct(ProductVM productVM);
        bool DeleteProduct(int productId);
        Product GetProduct(int productId);
        List<Product> GetProducts();
        Product UpdateProduct(int productId, Product productVM);
        // discount table
        Discount AddDiscount(Discount dicount);
        List<Discount> GetAllDiscount();
        Discount GetDiscount(int discountId);
        Discount UpdateDiscount(int discountId, Discount discount);
        bool DeleteDiscount(int discountId);
    }
}