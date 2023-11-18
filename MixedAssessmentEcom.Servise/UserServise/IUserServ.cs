using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.ViewModels.UsersViewModels;

namespace MixedAssessmentEcom.Servise.UserServise
{
    public interface IUserServ
    {

        Task<UserViewVm> AddUser(UserRegistrationVM userRegVM);
        UserViewVm GetUser(int userId);
        string LoginUser(UserLoginVM loginVm);
         string TokenValidate( string otpName);
         UserTokenDataVM UserRelatedData(string otpName);
        Task<string> UpdatePassword(string email);
        List<CountryVM> GetCountries();
        List<StateVM> GetStates(int countryId);
        User UpdateUser(int UserId, UserRegistrationVM userRegistrationVM);
        string ChangePassword(int userId, EmailVm emailVm);



    }
}