using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly UserContext _context;

        public ProductRepository(UserContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int productId)
        {
            var data = _context.Products.FirstOrDefault(d => d.ProductId == productId);
            return data;
        }

        // get product details by product name 
        public Product GetProductByProductName(string productName)
        {
            var data = _context.Products.FirstOrDefault(d => d.ProductName == productName);
            return data;
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product UpdateProduct(Product oldData, Product newData)
        {
            _context.Entry<Product>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }



        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
        
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        
        // Discount table start here 

        public Discount AddDiscount(Discount discount)
        {
            _context.DiscountDetails.Add(discount);
            _context.SaveChanges();
            return discount;
        }

        public List<Discount> GetAllDiscount()
        {
            return _context.DiscountDetails.ToList();
        }

        public Discount UpdateDiscount(Discount oldData, Discount newData)
        {
            _context.Entry<Discount>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }
        public Discount GetDiscount(int discountId)
        {
            var data = _context.DiscountDetails.FirstOrDefault(d => d.DiscountId == discountId);
            return data;
        }


        public void DeleteDiscount(Discount dis)
        {
            _context.DiscountDetails.Remove(dis);
            _context.SaveChanges();
        }




    }
}
