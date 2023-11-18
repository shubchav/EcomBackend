using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class SalesDetail
    {
        public int SaleDetailId { get; set; }
        //foreign key 
        public string InvoiceId { get; set; }
       // public SalesMaster SalesMaster { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int SalesQuantity { get; set; }
        public int NewStock { get; set; }
        public decimal SellingPrice { get; set; }
        
    }
}
