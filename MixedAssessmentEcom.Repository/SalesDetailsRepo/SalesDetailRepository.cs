using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.SalesDetailsRepo
{
    public class SalesDetailRepository : ISalesDetailRepository
    {
        private readonly UserContext _context;
        public SalesDetailRepository(UserContext context)
        {
            _context = context;
        }

        public SalesDetail AddSalesDetails(SalesDetail salesDetail)
        {
            _context.SalesDetails.Add(salesDetail);
            _context.SaveChanges();
            return salesDetail;
        }
        public List<SalesDetail> GetSaleDetails()
        {
            return _context.SalesDetails.ToList();
        }

        public SalesDetail GetSaleDetails(int salesDId)
        {
            var data = _context.SalesDetails.FirstOrDefault(d => d.SaleDetailId == salesDId);
            return data;
        }

        public List<SalesDetail> GetSalesDetailByinvoiceid(string invoiceId)
        {
            var data = _context.SalesDetails.Where(d => d.InvoiceId == invoiceId).ToList();
            return data;
        }
        public void DeleteSalesDetails(SalesDetail sales)
        {
            _context.SalesDetails.Remove(sales);
            _context.SaveChanges();
        }


    }
}
