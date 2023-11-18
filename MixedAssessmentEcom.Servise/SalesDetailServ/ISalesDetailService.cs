using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.ViewModels.SalesDetailViewModels;

namespace MixedAssessmentEcom.Servise.SalesDetailServ
{
    public interface ISalesDetailService
    {
        string AddSalesDetail(SalesDetailVM salesDetailVM);
        List<SalesDetail> GetSalesDetailByInvoiceId(string invoiceId);
    }
}