using AutoMapper;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.ViewModels;
using MixedAssessmentEcom.ViewModels.CartDetailsViewModels;
using MixedAssessmentEcom.ViewModels.CartMasterViewModels;
using MixedAssessmentEcom.ViewModels.ProductViewModel;
using MixedAssessmentEcom.ViewModels.SalesDetailViewModels;
using MixedAssessmentEcom.ViewModels.SalesMasterViewModels;
using MixedAssessmentEcom.ViewModels.UsersViewModels;

namespace MixedAssessmentEcom.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserViewVm, User>().ReverseMap();
            CreateMap<UserLoginVM, User>().ReverseMap();
            CreateMap<ProductVM, Product>().ReverseMap();
            //CreateMap<EmailVm, User>().ForMember(
            //   usr => usr.Password,
            //   opt => opt.MapFrom(src => src.Password));

            CreateMap<EmailVm, User>().ReverseMap();

            CreateMap<CountryVM, Country>().ReverseMap();
            CreateMap<StateVM, State>().ReverseMap();
            CreateMap<MasterCartVM, MasterCart>().ReverseMap();
            CreateMap<CartDetailsVM, CartDetail>().ReverseMap();
            CreateMap<PaymentCardVM, PaymentCard>().ReverseMap();
            CreateMap<SalesMasterVM,SalesMaster>().ReverseMap();
            CreateMap<SalesDetailVM, SalesDetail>().ReverseMap();
            CreateMap<SalesDetsilShowVM, SalesDetail>().ReverseMap();




        }
    }
}
