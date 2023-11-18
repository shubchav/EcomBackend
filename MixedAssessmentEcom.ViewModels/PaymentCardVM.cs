using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels
{
    public class PaymentCardVM
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        // 3 digit 
        public int CVV { get; set; }
    }
}
