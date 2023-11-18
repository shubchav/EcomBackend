using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels.ProductViewModel
{
    public class ProductVM
    {
       
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
        public int DiscountId   { get; set; }

    }
}
