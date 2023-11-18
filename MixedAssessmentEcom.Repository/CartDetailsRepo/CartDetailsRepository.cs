using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.CartDetailsRepo
{
    public class CartDetailsRepository : ICartDetailsRepository
    {
        private readonly UserContext _context;

        public CartDetailsRepository(UserContext context)
        {
            _context = context;
        }

        public List<CartDetail> GetCartDetails()
        {
            return _context.CartDetails.ToList();
        }

        public CartDetail GetCartDetail(int cartDetailId)
        {
            var data = _context.CartDetails.FirstOrDefault(d => d.CartDetailId == cartDetailId);
            return data;
        }


        public List<CartDetail> GetCartDetailByMasterId(int cartId)
        {
            var data = _context.CartDetails.Where(d => d.CartId == cartId).ToList();
            return data;
        }
        public CartDetail GetCartDetailByUserId(int userId)
        {

            var data = _context.CartDetails.FirstOrDefault(d => d.UserId == userId);
            return data;
        }
        //sales master service calling 
        public List<CartDetail> GetAllRelatedCartDetailByUserId(int userId)
        {

            var data = _context.CartDetails.Where(d => d.UserId == userId).ToList();
            return data;
        }
        // after that we get data by cartid and user id
        public List<CartDetail> GetAllRelatedCartDetailByUserIdAndCartId(int userId, int mastercartId)
        {

            var data = _context.CartDetails.Where(d => d.UserId == userId && d.CartId == mastercartId).ToList();
            return data;
        }
        public List<CartDetail> GetAllQuantityCartDetailByUserId(int userId)
        {

            var data = _context.CartDetails.OrderBy(d=>d.CartDetailId).Where(d => d.UserId == userId).ToList();
            return data;
        }
        public List<CartDetail> GetAllQuantityCartDetailByMCartId(int masterCartId)
        {

            var data = _context.CartDetails.Where(d => d.CartId== masterCartId).ToList();
            return data;
        }
        public CartDetail GetInvoiceLastIdByUserId(int userId)
        {

            var data = _context.CartDetails.OrderBy(d => d.CartDetailId).Where(d => d.UserId == userId).LastOrDefault();
            return data;
        }
        public CartDetail GetCartDetailByProductId(int productId)
        {
            var data = _context.CartDetails.FirstOrDefault(d => d.ProductId == productId);
            return data;
        }
        public CartDetail AddCartDetails(CartDetail cart)
        {
            _context.CartDetails.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        public CartDetail UpdateCartDetails(CartDetail oldData, CartDetail newData)
        {
            _context.Entry<CartDetail>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }

        public void DeleteCartDetails(CartDetail cart)
        {
            _context.CartDetails.Remove(cart);
            _context.SaveChanges();
        }

        public void DeleteCartDetailsByProductId(CartDetail cart)
        {
            _context.CartDetails.Remove(cart);
            _context.SaveChanges();
        }






    }
}
