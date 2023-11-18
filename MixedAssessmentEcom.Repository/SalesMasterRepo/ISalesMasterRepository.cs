using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.SalesMasterRepo
{
    public interface ISalesMasterRepository
    {
        SalesMaster AddSalesMaster(SalesMaster salesM);
        void DeleteSalesMaster(SalesMaster salesM);
        SalesMaster GetSaleMaster(int userId);
        List<SalesMaster> GetSaleMasters();
        OrderHistory AddOrderHistory(OrderHistory orderHistory);
        void DeleteOrderHistory(OrderHistory orderHistory);
        OrderHistory GetOrderHistory(int orderId);
        List<OrderHistory> GetOrderDetailsByInvoiceId(string invoiceId);
        SalesMaster UpdateSalesMaster(SalesMaster oldData, SalesMaster newData);
        List<OrderHistory> GetOrderDetailsByUserId(int userId);
        SalesMaster GetSaleMasterByInvoiceId(string InvoiceId);
    }
}