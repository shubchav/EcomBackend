using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.ProductRepo
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        void DeleteProduct(Product product);
        Product GetProduct(int productId);
        List<Product> GetProducts();
        Product UpdateProduct(Product oldData, Product newData);
        // Discount table 
        void DeleteDiscount(Discount dis);
        Discount UpdateDiscount(Discount oldData, Discount newData);
        List<Discount> GetAllDiscount();
        Discount GetDiscount(int discountId);
        Discount AddDiscount(Discount discount);
        // get product details by productName 
        Product GetProductByProductName(string productName);
    }
}