using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.SalesDetailsRepo
{
    public interface ISalesDetailRepository
    {
        SalesDetail AddSalesDetails(SalesDetail salesDetail);
        void DeleteSalesDetails(SalesDetail sales);
        List<SalesDetail> GetSaleDetails();
        SalesDetail GetSaleDetails(int salesDId);
        List<SalesDetail> GetSalesDetailByinvoiceid(string invoiceId);
    }
}