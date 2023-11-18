using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.MasterCartRepo
{
    public interface IMasterCartRepository
    {
        MasterCart AddMasterCart(MasterCart cart);
        List<MasterCart> GetAllMasterCartdetails();
        MasterCart GetMCartByUId(int userId);
        MasterCart UpdateMCart(MasterCart oldData, MasterCart newData);
        MasterCart GetMCartByMId(int cartMId);

    }
}