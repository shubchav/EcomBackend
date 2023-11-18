using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels.SalesMasterViewModels
{
    public class SalesMasterVM
    {
        public int SaleMasterId { get; set; }
        public string InvoiceId { get; set; }
        public int UserId { get; set; }
        public int MasterCartId { get; set; }
        // today date
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryZipcode { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryState { get; set; }
        
    }
}
