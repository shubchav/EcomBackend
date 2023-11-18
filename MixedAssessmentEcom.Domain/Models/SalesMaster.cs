using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public  class SalesMaster
    {
        public int SaleMasterId { get; set; }
        public string InvoiceId { get; set; }
        public int UserId { get; set; }
        // today date
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryZipcode { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryState { get; set; }
        //public SalesDetail SalesDetails { get; set; }





    }
}
