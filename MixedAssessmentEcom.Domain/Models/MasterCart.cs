using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class MasterCart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public bool IsPaymentDone { get; set; }
        //public List<CartDetail> CartDetails { get; set; }
       

    }


}
