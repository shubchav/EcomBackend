using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.MasterCartRepo
{
    public class MasterCartRepository : IMasterCartRepository
    {
        private readonly UserContext _context;

        public MasterCartRepository(UserContext context)
        {
            _context = context;
        }

        public List<MasterCart> GetAllMasterCartdetails()
        {
            return _context.Carts.ToList();
        }

        public  MasterCart  GetMCartByUId(int userId)
        {
            //before code
            // var data = _context.Carts.OrderByDescending(d => d.UserId == userId).LastOrDefault();
            // After code
            var data = _context.Carts.Where(d => d.UserId == userId).OrderBy(d => d.CartId).LastOrDefault();


            return data;
        }

        // get detail by  cart id 
        public MasterCart GetMCartByMId(int cartMId)
        {
            var data = _context.Carts.FirstOrDefault(d => d.CartId == cartMId);
            return data;
        }




        public MasterCart AddMasterCart(MasterCart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        public MasterCart UpdateMCart(MasterCart oldData, MasterCart newData)
        {
            _context.Entry<MasterCart>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }

    }
}
