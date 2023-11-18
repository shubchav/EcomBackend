using MixedAssessmentEcom.ViewModels.CartMasterViewModels;

namespace MixedAssessmentEcom.Servise.MasterCartServ
{
    public interface IMasterCartService
    {
        MasterCartVM AddMasterCart(MasterCartVM cartVM);
        List<MasterCartVM> GetAllMasterCart();
        MasterCartVM GetMCartDByUId(int userId);
        MasterCartVM UpdateMasterCart(int mastercartId, MasterCartVM mastercartVM);
    }
}