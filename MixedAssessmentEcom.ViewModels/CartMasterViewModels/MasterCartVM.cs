using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels.CartMasterViewModels
{
    public class MasterCartVM
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public bool IsPaymentDone { get; set; }

    }
}
