using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.ViewModels.OrderHistoryVM;
using MixedAssessmentEcom.ViewModels.SalesMasterViewModels;

namespace MixedAssessmentEcom.Servise.SalesMasterServ
{
    public interface ISalesMasterService
    {
        SalesMasterVM AddSalesMaster(SalesMasterVM saleMVM);
        bool DeleteSalesM(int salemasterId);
        List<SalesMasterVM> GetAllSalesMaster();
        SalesMasterVM GetSalesMaster(int userId);
        string AddOrderHistory(OrderHistoryUserIdVM orderHistory);
        bool DeleteOrderHistory(int OrderId);
        List<OrderHistory> GetAllOrderHstory(string invoiceId);
        string UpdateSalesMaster(int userId, SalesMasterVM salesVM);
        List<OrderHistory> GetAllOrderHstoryByUserId(int userId);
        SalesMasterVM GetSalesMasterByInvoiceID(string InvoiceID);
    }
}