using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels.SalesDetailViewModels
{
    public class SalesDetsilShowVM
    {
        public int SaleDetailId { get; set; }
        //foreign key 
        public int InvoiceId { get; set; }
      
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int SalesQuantity { get; set; }
        public int NewStock { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
