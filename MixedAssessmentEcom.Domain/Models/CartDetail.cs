using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
     public class CartDetail
     {
        public int CartDetailId { get; set; }
        //foreign key of cart table
        public int CartId { get; set; }
        public int UserId { get; set; }
        //public MasterCart Cart { get; set; }
        // foreign key of product table
        public int ProductId { get; set; }  
       // public Product Product { get; set; }
       // public List<Product> Products { get; set; }
        public int Quantity { get; set; }

     }
}

