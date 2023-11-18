using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class OrderHistory
    {

        public int OrderId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
       
//Display the Order listing with below fields, which we saved.: Display only Confirmed order
//Invoice No.
//Invoice Date
//Delivery date
//Below Items will display in the internal list as in one order there will be multiple products.
//Item image
//Item name
//Qty
//Price
    }
}
