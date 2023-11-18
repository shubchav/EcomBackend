using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.SalesMasterRepo
{
    public class SalesMasterRepository : ISalesMasterRepository
    {
        private readonly UserContext _context;
        public SalesMasterRepository(UserContext context)
        {
            _context = context;
        }


        public List<SalesMaster> GetSaleMasters()
        {
            return _context.SalesMasters.ToList();
        }

        public SalesMaster GetSaleMaster(int userId)
        {
            var data = _context.SalesMasters.Where(d => d.UserId == userId).OrderBy(d => d.SaleMasterId).LastOrDefault();
            return data;
        }
        public SalesMaster GetSaleMasterByInvoiceId(string InvoiceId)
        {
            var data = _context.SalesMasters.Where(d => d.InvoiceId == InvoiceId).OrderBy(d => d.SaleMasterId).LastOrDefault()
                ;
            return data;
        }

        public SalesMaster AddSalesMaster(SalesMaster salesM)
        {
            _context.SalesMasters.Add(salesM);
            _context.SaveChanges();
            return salesM;
        }

        //public Product UpdateProduct(Product oldData, Product newData)
        //{
        //    _context.Entry<Product>(oldData).CurrentValues.SetValues(newData);
        //    _context.SaveChanges();
        //    return newData;
        //}



        public void DeleteSalesMaster(SalesMaster salesM)
        {
            _context.SalesMasters.Remove(salesM);
            _context.SaveChanges();
        }

        //update the Salesmaster Table
        public SalesMaster UpdateSalesMaster(SalesMaster oldData, SalesMaster newData)
        {
            _context.Entry<SalesMaster>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }



        // Order History Table 
        public OrderHistory AddOrderHistory(OrderHistory orderHistory)    
        {
            _context.OrderHistory.Add(orderHistory);
            _context.SaveChanges();
            return orderHistory;
        }

        public void DeleteOrderHistory(OrderHistory orderHistory)
        {
            _context.OrderHistory.Remove(orderHistory);
            _context.SaveChanges();
        }
        public OrderHistory GetOrderHistory(int orderId)
        {
            var data = _context.OrderHistory.Where(d => d.OrderId == orderId).FirstOrDefault();
            return data;
        }
        // detail by invoice Id
        public List<OrderHistory> GetOrderDetailsByInvoiceId( string invoiceId)
        {
            var data = _context.OrderHistory.Where(d=> d.InvoiceNumber == invoiceId).ToList();
            return data;
        }

        // detail by userid

        public List<OrderHistory> GetOrderDetailsByUserId(int userId)
        {
            var data = _context.OrderHistory.Where(d => d.UserId == userId).ToList();
            return data;
        }
    }
}

