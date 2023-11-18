using AutoMapper;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.ProductRepo;
using MixedAssessmentEcom.ViewModels.ProductViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Servise.ProductServ
{
    public class ProductServise : IProductServise
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;
        public ProductServise(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        public List<Product> GetProducts()
        {
            var pol = _productRepo.GetProducts();
            var polvm = _mapper.Map<List<Product>>(pol);
            return polvm;
        }

        public Product GetProduct(int productId)
        {
            var product = _productRepo.GetProduct(productId);
            var productvm = _mapper.Map<Product>(product);

            return productvm;
        }

        public ProductVM AddProduct(ProductVM productVM)

        {
            var discountData = _productRepo.GetDiscount(productVM.DiscountId);
            var discountPrice = productVM.SellingPrice - (productVM.SellingPrice * ((decimal)(discountData.DiscountRate ))/ 100);
            var productClass = new Product();
            if (discountData.Status == "Active")
            {


                productClass.ProductId = 0;
                productClass.ProductName = productVM.ProductName;
                productClass.ProductCode = productVM.ProductCode;
                productClass.ProductImage = productVM.ProductImage;
                productClass.Category = productVM.Category;
                productClass.Brand = productVM.Brand;
                productClass.SellingPrice = discountPrice;
                productClass.PurchasePrice = productVM.SellingPrice;
                productClass.SellingDate = productVM.SellingDate;
                productClass.Stock = productVM.Stock;
                productClass.DiscountId = productVM.DiscountId;


            }
            //if(discountData.Status == "Deactive")
            else 
            {
                productClass.ProductId = 0;
                productClass.ProductName = productVM.ProductName;
                productClass.ProductCode = productVM.ProductCode;
                productClass.ProductImage = productVM.ProductImage;
                productClass.Category = productVM.Category;
                productClass.Brand = productVM.Brand;
                productClass.SellingPrice = productVM.SellingPrice;
                productClass.PurchasePrice = productVM.PurchasePrice;
                productClass.SellingDate = productVM.SellingDate;
                productClass.Stock = productVM.Stock;
                productClass.DiscountId = productVM.DiscountId;
            }
            var prodt = _mapper.Map<Product>(productVM);
            _productRepo.AddProduct(productClass);
            var prodtVM = _mapper.Map<ProductVM>(productClass);
            return prodtVM;


        }

        public Product UpdateProduct(int productId, Product productVM)
        {

            var exitdata = _productRepo.GetProduct(productId);

            var productClass = new Product()
            {
                ProductId = exitdata.ProductId,
                ProductName = productVM.ProductName,
                ProductCode = productVM.ProductCode,
                Category = productVM.Category,
                Brand = productVM.Brand,
                SellingPrice = productVM.SellingPrice,
                PurchasePrice = productVM.PurchasePrice,
                SellingDate = productVM.SellingDate,
                ProductImage = exitdata.ProductImage,
                Stock = productVM.Stock,
                //DiscountId= productVM.DiscountId,

            };

            // data class type
            //var productdata = _mapper.Map<Product>(productVM);

            // repo calling 
            _productRepo.UpdateProduct(exitdata, productClass);
            // data of vm type 
            var productVMDAta = _mapper.Map<Product>(productClass);

            return productVMDAta;
        }

        // update the quantity in product when order is comformed 
       


        public bool DeleteProduct(int productId)
        {
            var del = _productRepo.GetProduct(productId);

            _productRepo.DeleteProduct(del);
            return true;
        }


        // Discount part is start  ////////////////////////////////////////////////////////////////////////////
        public Discount AddDiscount(Discount dicount)

        {
           var data= _productRepo.AddDiscount(dicount); 
            return data;
        }


        public List<Discount> GetAllDiscount()
        {
            var pol = _productRepo.GetAllDiscount();
           
            return pol;
        }

        public Discount GetDiscount(int discountId)
        {
            var product = _productRepo.GetDiscount(discountId);
            return product;
        }

     

        public Discount UpdateDiscount(int discountId, Discount discount)
        {

            var exitdata = _productRepo.GetDiscount(discountId);

            var discountClass = new Discount()
            {
               DiscountId = exitdata.DiscountId,
                DiscountName = discount.DiscountName,
                DiscountRate = discount.DiscountRate,
                Status = discount.Status,

            };
            _productRepo.UpdateDiscount(exitdata, discountClass);
            return discountClass;
        }

        public bool DeleteDiscount(int discountId)
        {
            var del = _productRepo.GetDiscount(discountId);

            _productRepo.DeleteDiscount(del);
            return true;
        }
    }
}
