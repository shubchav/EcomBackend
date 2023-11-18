using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public string DiscountName { get; set; }
        public double DiscountRate { get; set; }
        public string Status { get; set; }
       // Discount Name, Discount Rate ,Active(status of the Discount)
    }
}
