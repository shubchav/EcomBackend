using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //6 digit alphanumeric
        public string ProductCode { get; set; }
        public string ProductImage { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime SellingDate { get; set; }
        public int Stock { get; set; }
        public int DiscountId { get; set; }
        //public int CartDetailId { get; set; }
        //public CartDetail CartDetail { get; set; }
    }  
}
//Product Name, Product Code(unique)(alpha numeric-6character)
//,Product image(image upload)-optional field,Category(string)
//Brand(string),Selling Price(Float), Purchase Price(float), Selling Date, Stock(Integer)