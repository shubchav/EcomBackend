using AutoMapper;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.CartDetailsRepo;
using MixedAssessmentEcom.Repository.ProductRepo;
using MixedAssessmentEcom.Repository.SalesMasterRepo;
using MixedAssessmentEcom.Repository.UserRepository;
using MixedAssessmentEcom.ViewModels.OrderHistoryVM;
using MixedAssessmentEcom.ViewModels.SalesMasterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Servise.SalesMasterServ
{
    public class SalesMasterService : ISalesMasterService
    {
        private readonly ISalesMasterRepository _salesMSer;
        private readonly IMapper _mapper;
        private readonly ICartDetailsRepository _cartDetailRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepo _userRepo;
        public SalesMasterService(ISalesMasterRepository salesMSer, IMapper mapper , ICartDetailsRepository cartDetailRepo, IProductRepository productRepo, IUserRepo userRepo)
        {
            _salesMSer = salesMSer;
            _mapper = mapper;
            _cartDetailRepo = cartDetailRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
        }

        public List<SalesMasterVM> GetAllSalesMaster()
        {
            var pol = _salesMSer.GetSaleMasters();
            var polvm = _mapper.Map<List<SalesMasterVM>>(pol);
            return polvm;
        }

        public SalesMasterVM GetSalesMaster(int userId)
        {
            var sales = _salesMSer.GetSaleMaster(userId);
            var salesvm = _mapper.Map<SalesMasterVM>(sales);

            return salesvm;
        }

        public SalesMasterVM GetSalesMasterByInvoiceID(string InvoiceID)
        {
            var sales = _salesMSer.GetSaleMasterByInvoiceId(InvoiceID);
            var salesvm = _mapper.Map<SalesMasterVM>(sales);

            return salesvm;
        }
        public SalesMasterVM AddSalesMaster(SalesMasterVM saleMVM)

        {
            string invoiceId = "";
            var countid = _salesMSer.GetSaleMasters().Count();
            if (countid >= 1 && countid < 10)
            {
                invoiceId = "-ORD - 00" + (countid + 1).ToString();
            }
            if (countid >= 10 && countid < 100)
            {
                invoiceId = "-ORD - 0" + (countid + 1).ToString();

            }
            if (countid >= 100 && countid < 1000)
            {
                invoiceId = "-ORD - " + (countid + 1).ToString();

            }
            if (countid >= 1000 && countid < 10000)
            {
                invoiceId = "-ORD - " + (countid + 1).ToString();

            }

            // here we have use of masterId not user id
            //before
            //var cartdetailDataCount = _cartDetailRepo.GetAllRelatedCartDetailByUserId(saleMVM.UserId).Count();

            //var cartdetailData =_cartDetailRepo.GetAllRelatedCartDetailByUserId(saleMVM.UserId);

            //After

            var cartdetailDataCount = _cartDetailRepo.GetCartDetailByMasterId(saleMVM.MasterCartId).Count();
            var cartdetailData = _cartDetailRepo.GetCartDetailByMasterId(saleMVM.MasterCartId);
            int pId ;
            int qty;
           // var productdata="";
            decimal price;
            decimal total;
            decimal allTotal=0;
           for (int i = 0; i < cartdetailDataCount; i++)
            {
                 pId = cartdetailData[i].ProductId;
                 qty = cartdetailData[i].Quantity;
                var productdata = _productRepo.GetProduct(pId);
                 price = productdata.SellingPrice;
                 total = qty * price;
                allTotal = allTotal+ total;
            }

            //var pId = cartdetailData[0].ProductId;
            //var qty = cartdetailData[0].Quantity;
            //var productdata = _productRepo.GetProduct(pId);
            //var price = productdata.SellingPrice;
            //var total = qty * price;

            //var pId2 = cartdetailData[1].ProductId;
            //var qty2 = cartdetailData[1].Quantity;
            //var productdata2 = _productRepo.GetProduct(pId2);
            //var price2 = productdata2.SellingPrice;
            //var total2 = qty2 * price2;


            //var pId3 = cartdetailData[2].ProductId;
            //var qty3 = cartdetailData[2].Quantity;
            //var productdata3 = _productRepo.GetProduct(pId3);
            //var price3 = productdata3.SellingPrice;
            //var total3 = qty3 * price3;

           var userdata = _userRepo.GetUser(saleMVM.UserId);

            var saleMClass = new SalesMaster()
            {
                SaleMasterId = saleMVM.SaleMasterId,
                InvoiceId = invoiceId,
                UserId = saleMVM.UserId,
                InvoiceDate = DateTime.Now.Date,
                Subtotal = allTotal,
                DeliveryAddress = userdata.Address,
                DeliveryZipcode = userdata.Zipcode.ToString(),
                DeliveryCountry = userdata.Country,
                DeliveryState = userdata.State

            };
            _salesMSer.AddSalesMaster(saleMClass);

            var salevm = _mapper.Map<SalesMasterVM>(saleMClass);
            return salevm;


        }



        public bool DeleteSalesM(int salemasterId)
        {
            var del = _salesMSer.GetSaleMaster(salemasterId);

            _salesMSer.DeleteSalesMaster(del);
            return true;
        }


        //update sales master
        public string UpdateSalesMaster(int userId, SalesMasterVM salesVM)
        {

            var exitdata = _salesMSer.GetSaleMaster(userId);

            var salesMasterClass = new SalesMaster()
            {
                SaleMasterId = exitdata.SaleMasterId,
                InvoiceId = exitdata.InvoiceId,
                UserId = exitdata.UserId,
                InvoiceDate = exitdata.InvoiceDate,
                Subtotal = 0,
                DeliveryAddress = exitdata.DeliveryAddress,
                DeliveryZipcode = exitdata.DeliveryZipcode.ToString(),
                DeliveryCountry = exitdata.DeliveryCountry,
                DeliveryState = exitdata.DeliveryState

            };

            _salesMSer.UpdateSalesMaster(exitdata, salesMasterClass);

            return "Update Successfully.";
            
        }



        /// Order History part start 
        /// 
        public string AddOrderHistory(OrderHistoryUserIdVM orderHistory)
        {
           
            var cartdetailDataCount = _cartDetailRepo.GetAllRelatedCartDetailByUserIdAndCartId(orderHistory.UserId, orderHistory.MasterCartId).Count();

            var cartdetailData = _cartDetailRepo.GetAllRelatedCartDetailByUserIdAndCartId(orderHistory.UserId, orderHistory.MasterCartId);
            var getInvoiceId = _salesMSer.GetSaleMaster(orderHistory.UserId);
            int pId;
           // int qty;

            var deliveryDate = getInvoiceId.InvoiceDate.AddDays(10);
            var orderHistoryC = new OrderHistory();
            var productupt = new Product();
            // var productdata="";
            //decimal price;
            //decimal total;
            //decimal allTotal = 0;

            //for (int i = 0; i < cartdetailDataCount; i++)
            //{
            //    pId = cartdetailData[i].ProductId;
            //    qty = cartdetailData[i].Quantity;
            //    var productdata = _productRepo.GetProduct(pId);
            //    price = productdata.SellingPrice;
            //    total = qty * price;
                
            //    //allTotal = allTotal + total;
            //}

            foreach (var data in cartdetailData)
            {

                orderHistoryC.OrderId = 0;
                orderHistoryC.UserId = orderHistory.UserId;
                orderHistoryC.InvoiceNumber = getInvoiceId.InvoiceId;
                orderHistoryC.InvoiceDate=getInvoiceId.InvoiceDate;
                orderHistoryC.DeliveryDate = deliveryDate;
                pId = data.ProductId;
                var productdata = _productRepo.GetProduct(pId);
                // updated part
                var uptQty = productdata.Stock - data.Quantity;
                // product part
                productupt.ProductId = productdata.ProductId;
                productupt.ProductName = productdata.ProductName;
                productupt.ProductCode = productdata.ProductCode;
                productupt.Category = productdata.Category;
                productupt.Brand = productdata.Brand;
                productupt.SellingPrice = productdata.SellingPrice;
                productupt.PurchasePrice = productdata.PurchasePrice;
                productupt.SellingDate = productdata.SellingDate;
                productupt.ProductImage = productdata.ProductImage;
                productupt.Stock = uptQty;
                productdata.DiscountId = productdata.DiscountId;
                    // updated part end
                orderHistoryC.ProductImage = productdata.ProductImage;
                orderHistoryC.ProductName = productdata.ProductName;
                orderHistoryC.Qty = data.Quantity;
                orderHistoryC.Price = productdata.SellingPrice * orderHistoryC.Qty;
                _salesMSer.AddOrderHistory(orderHistoryC);
                //call the product update repository
                _productRepo.UpdateProduct(productdata, productupt);
            };

            //var pId = cartdetailData[0].ProductId;


            return "Order History Add Successfully. ";


        }



        //Order History delete

        public bool DeleteOrderHistory(int OrderId)
        {

            var del = _salesMSer.GetOrderHistory(OrderId);
            var productdata = _productRepo.GetProductByProductName(del.ProductName);
            var uptQty = productdata.Stock + del.Qty;
            var udtProduct = new Product()
            {
                ProductId = productdata.ProductId,
                ProductName = productdata.ProductName,
                ProductCode = productdata.ProductCode,
                Category = productdata.Category,
                Brand = productdata.Brand,
                SellingPrice = productdata.SellingPrice,
                PurchasePrice = productdata.PurchasePrice,
                SellingDate = productdata.SellingDate,
                ProductImage = productdata.ProductImage,
                Stock = uptQty,
                DiscountId = productdata.DiscountId,
        };
            _productRepo.UpdateProduct(productdata, udtProduct);
            _salesMSer.DeleteOrderHistory(del);
            return true;
        }
        // by invoiceId
        public List<OrderHistory> GetAllOrderHstory (string invoiceId)
        {
          var data = _salesMSer.GetOrderDetailsByInvoiceId(invoiceId);
          return data;
        }
        // by user Id
        public List<OrderHistory> GetAllOrderHstoryByUserId(int userId)
        {
            var data = _salesMSer.GetOrderDetailsByUserId(userId);
            return data;
        }


    }
}
