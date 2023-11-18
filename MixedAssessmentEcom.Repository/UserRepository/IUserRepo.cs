using MixedAssessmentEcom.Domain.Models;

namespace MixedAssessmentEcom.Repository.UserRepository
{
    public interface IUserRepo
    {
        User AddUser(User user);
        User GetUser(int userId);
        User UserLogin(User user);
        public UserOtp GetOtp(string otpname);
        void AddOtp(UserOtp otpValue);
        UserOtp GetUserRelatedData(string otpname);
        User EmailVerification(string email);
        void UpdatePassword(User oldp, User newp);

        List<Country> GetCountries();
        List<State> GetStates(int countryId);
        User UpdateUser(User oldData, User newData);
        User ChangePassword(User oldData, User newData);
    }
}