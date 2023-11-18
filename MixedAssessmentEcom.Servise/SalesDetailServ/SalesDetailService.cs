using AutoMapper;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.CartDetailsRepo;
using MixedAssessmentEcom.Repository.ProductRepo;
using MixedAssessmentEcom.Repository.SalesDetailsRepo;
using MixedAssessmentEcom.Repository.SalesMasterRepo;
using MixedAssessmentEcom.ViewModels.SalesDetailViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Servise.SalesDetailServ
{
    public class SalesDetailService : ISalesDetailService
    {
        private readonly ISalesDetailRepository _salesDetailRepo;
        private readonly IMapper _mapper;

        private readonly ISalesMasterRepository _salesMSer;
        private readonly ICartDetailsRepository _cartDetailRepo;
        private readonly IProductRepository _productRepo;

        public SalesDetailService(ISalesDetailRepository salesDetailRepo, IMapper mapper, ISalesMasterRepository salesMSer, ICartDetailsRepository cartDetailRepo, IProductRepository productRepo)
        {
            _salesDetailRepo = salesDetailRepo;
            _mapper = mapper;
            _salesMSer = salesMSer;
            _cartDetailRepo = cartDetailRepo;
            _productRepo = productRepo;
        }

        public string AddSalesDetail(SalesDetailVM salesDetailVM)
        {
            var sales = _salesMSer.GetSaleMaster(salesDetailVM.UserId);
            var cartDetailsData = _cartDetailRepo.GetCartDetailByMasterId(salesDetailVM.MasterCartId);
            var cartDetailsDataCount = _cartDetailRepo.GetCartDetailByMasterId(salesDetailVM.MasterCartId).Count();

            var invoice = sales.InvoiceId;


            var salesdetailClass = new SalesDetail();


            foreach (var data in cartDetailsData)
            {
                salesdetailClass.SaleDetailId = 0;
                salesdetailClass.InvoiceId = sales.InvoiceId;

                salesdetailClass.ProductId = data.ProductId;
                var ProductDetail = _productRepo.GetProduct(salesdetailClass.ProductId);
                salesdetailClass.ProductCode = ProductDetail.ProductCode;
                salesdetailClass.SalesQuantity = data.Quantity;
                salesdetailClass.NewStock = ProductDetail.Stock - salesdetailClass.SalesQuantity;
                salesdetailClass.SellingPrice = ProductDetail.SellingPrice * data.Quantity;
                _salesDetailRepo.AddSalesDetails(salesdetailClass);
            };

            //_salesDetailRepo.AddSalesDetails(salesdetailClass);

            //var salevm = _mapper.Map<List<SalesDetsilShowVM>>(salesdetailClass);

            return "Data Added Successfully.";
        }

        public List<SalesDetail> GetSalesDetailByInvoiceId(string invoiceId)
        {
            var data = _salesDetailRepo.GetSalesDetailByinvoiceid(invoiceId);
            return data;
        }

    }
}
