using AutoMapper;
using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.MasterCartRepo;
using MixedAssessmentEcom.ViewModels.CartMasterViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Servise.MasterCartServ
{
    public class MasterCartService : IMasterCartService
    {
        private readonly IMasterCartRepository _masterCartRepo;
        private readonly IMapper _mapper;

        public MasterCartService(IMasterCartRepository masterCartRepo, IMapper mapper)
        {
            _masterCartRepo = masterCartRepo;
            _mapper = mapper;
        }

        public List<MasterCartVM> GetAllMasterCart()
        {
            var cart = _masterCartRepo.GetAllMasterCartdetails();
            var cartvm = _mapper.Map<List<MasterCartVM>>(cart);
            return cartvm;
        }

        public MasterCartVM GetMCartDByUId(int userId)
        {
            var cartMaster = _masterCartRepo.GetMCartByUId(userId);
            var cartMasterVm = _mapper.Map<MasterCartVM>(cartMaster);

            return cartMasterVm;
        }

        public MasterCartVM AddMasterCart(MasterCartVM cartVM)

        {
            var cartIddetails = _masterCartRepo.GetMCartByUId(cartVM.UserId); 
            if(cartIddetails == null || (cartVM.IsPaymentDone == true && cartIddetails.UserId == cartVM.UserId))
            {
                var prodt = _mapper.Map<MasterCart>(cartVM);

                var data = _masterCartRepo.AddMasterCart(prodt);
                var prodtVM = _mapper.Map<MasterCartVM>(data);
                return prodtVM;
            }
            return cartVM;
            //if(cartVM.CartId==cartIddetails.CartId && cartVM.UserId== cartIddetails.UserId && cartVM.IsPaymentDone == car  )
            


        }
        public MasterCartVM UpdateMasterCart(int mastercartId, MasterCartVM mastercartVM)
        {

            var exitdata = _masterCartRepo.GetMCartByMId(mastercartId);

            var masterCartData = new MasterCart()
            {
                CartId = exitdata.CartId,
                UserId = exitdata.UserId,
                IsPaymentDone = mastercartVM.IsPaymentDone,
            };
            _masterCartRepo.UpdateMCart(exitdata, masterCartData);
            var vmdata = _mapper.Map<MasterCartVM>(masterCartData);

            return vmdata;



        }

    }
}
