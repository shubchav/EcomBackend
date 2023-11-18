using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels.CartDetailsViewModels
{
    public  class CartDetailsVM
    {
        public int CartDetailId { get; set; }
        //foreign key of cart table
        public int CartId { get; set; }
        public int UserId { get; set; }
      
        
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
    }
}
