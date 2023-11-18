    using AutoMapper;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.CartDetailsRepo;
using MixedAssessmentEcom.Repository.ProductRepo;
using MixedAssessmentEcom.ViewModels.CartDetailsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Servise.CartDetailsServ
{
    public class CartDetailsService : ICartDetailsService
    {
        private readonly ICartDetailsRepository _cartDetailRepo;
        private readonly IMapper _mapper;
        // product repo
        private readonly IProductRepository _productRepo;
        public CartDetailsService(ICartDetailsRepository cartDetailRepo, IMapper mapper, IProductRepository productRepo)
        {
            _cartDetailRepo = cartDetailRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public List<CartDetailsVM> GetAllCartDetsils()
        {
            var pol = _cartDetailRepo.GetCartDetails();
            var polvm = _mapper.Map<List<CartDetailsVM>>(pol);
            return polvm;
        }

        

        public CartDetailsVM GetCartDetail(int cartDetailId)
        {
            var product = _cartDetailRepo.GetCartDetail(cartDetailId);
            var productvm = _mapper.Map<CartDetailsVM>(product);

            return productvm;
        }


        public List<CartDetailsVM> GetCartDetailByCartId(int cartId)
        {
            var product = _cartDetailRepo.GetCartDetailByMasterId(cartId);
            var productvm = _mapper.Map<List<CartDetailsVM>>(product);

            return productvm;
        }
        // make now 
        public List<CartDetailsVM> GetAllRelatedCartDetailByUserId(int userId)
        {
            var product = _cartDetailRepo.GetAllRelatedCartDetailByUserId(userId);
            var productvm = _mapper.Map<List<CartDetailsVM>>(product);

            return productvm;
        }
        

        public CartDetailsVM GetCartDetailByUserId(int userId)
        {
            var product = _cartDetailRepo.GetCartDetailByUserId(userId);
            var productvm = _mapper.Map<CartDetailsVM>(product);

            return productvm;
        }
        public List<CartDetailsVM>GetQuantityCartDetailByUserId(int userId)
        {
            var product = _cartDetailRepo.GetAllQuantityCartDetailByUserId(userId);
            var productvm = _mapper.Map<List<CartDetailsVM>>(product);

            return productvm;
        }

        // by cartid
        public List<CartDetailsVM> GetQuantityCartDetailByMCartId(int mastrerCartId)
        {
            var product = _cartDetailRepo.GetAllQuantityCartDetailByMCartId(mastrerCartId);
            var productvm = _mapper.Map<List<CartDetailsVM>>(product);

            return productvm;
        }
        public CartDetailsVM AddCartDetail(CartDetailsVM cartDetailVM)

            {
            var prodt = _mapper.Map<CartDetail>(cartDetailVM);

            _cartDetailRepo.AddCartDetails(prodt);

            var prodtVM = _mapper.Map<CartDetailsVM>(prodt);
            return prodtVM;


        }
        // update for adding a quantity in a cart

        public string IncreaseUpdateQuantiy(int cartDetailId, CartDetailQtyUpdateVm cartDVM)
        {

            var exitdata = _cartDetailRepo.GetCartDetail(cartDetailId);
            var productdata = _productRepo.GetProduct(exitdata.ProductId);
            var cartDClass = new CartDetail();
            if (productdata.Stock > exitdata.Quantity)
            {
               
                {
                    cartDClass.CartDetailId = exitdata.CartDetailId;
                    cartDClass.UserId = exitdata.UserId;
                    cartDClass.CartId = exitdata.CartId;
                    cartDClass.ProductId = exitdata.ProductId;
                    cartDClass.Quantity = exitdata.Quantity + 1;
                };
                _cartDetailRepo.UpdateCartDetails(exitdata, cartDClass);
                var cartDetaildata = _mapper.Map<CartDetailsVM>(cartDClass);
                return "Quantity Added.";
            }
           else
            {
                return "Stock is not Available";
            }

            // repo calling 
            // data of vm type 

           
        }

        // update for subtracting a quantity in cart
        public string DecreaseUpdateQuantiy(int cartDetailId, CartDetailQtyUpdateVm cartDVM)
        {

            var exitdata = _cartDetailRepo.GetCartDetail(cartDetailId);
            var productdata = _productRepo.GetProduct(exitdata.ProductId);
            var cartDClass = new CartDetail();
            if (productdata.Stock >= exitdata.Quantity && exitdata.Quantity > 1)
            {

                {
                    cartDClass.CartDetailId = exitdata.CartDetailId;
                    cartDClass.UserId = exitdata.UserId;
                    cartDClass.CartId = exitdata.CartId;
                    cartDClass.ProductId = exitdata.ProductId;
                    cartDClass.Quantity = exitdata.Quantity - 1;
                };
                _cartDetailRepo.UpdateCartDetails(exitdata, cartDClass);
                var cartDetaildata = _mapper.Map<CartDetailsVM>(cartDClass);
                return " Quantity Remove. ";
            }
            else
            {
                return "Stock is not Available";
            }
        }

        public CartDetailsVM UpdateCartDetail(int cartDetailId, CartDetailsVM cartDVM)
        {

            var exitdata = _cartDetailRepo.GetCartDetail(cartDetailId);

            var cartDClass = new CartDetail()
            {
                CartDetailId = exitdata.CartDetailId,
                UserId = cartDVM.UserId,
                CartId = cartDVM.CartId,
                ProductId = cartDVM.ProductId,
                Quantity = cartDVM.Quantity,
            };


            // data class type
            //var productdata = _mapper.Map<Product>(productVM);

            // repo calling 
            _cartDetailRepo.UpdateCartDetails(exitdata, cartDClass);
            // data of vm type 
            var cartDetaildata = _mapper.Map<CartDetailsVM>(cartDClass);

            return cartDetaildata;
        }

        public bool DeleteCartDetail(int cartDetailId)
        {
            var del = _cartDetailRepo.GetCartDetail(cartDetailId);

            _cartDetailRepo.DeleteCartDetails(del);
            return true;
        }

        public bool DeleteCartDetailByProductId(int productId)
        {
            var del = _cartDetailRepo.GetCartDetailByProductId(productId);

            _cartDetailRepo.DeleteCartDetails(del);
            return true;
        }


    }
}
