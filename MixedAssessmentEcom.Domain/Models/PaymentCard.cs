using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public  class PaymentCard
    {

        public int CardId { get; set; }
        // 16digit
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        // 3 digit 
        public int CVV { get; set; }
    }
}
