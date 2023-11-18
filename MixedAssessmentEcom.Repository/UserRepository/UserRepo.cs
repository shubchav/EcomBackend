using Microsoft.EntityFrameworkCore;
using MixedAssessmentEcom.Domain.Context;
using MixedAssessmentEcom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Repository.UserRepository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext _context;

        public UserRepo(UserContext context)
        {
            _context = context;
        }

        // otp repository code for getting otp 
        // and post otp for reverification

        public UserOtp GetOtp(string otpname)
        {
            var otpdata = _context.UserOtps.FirstOrDefault(s => s.OtpName == otpname);
            return otpdata;
        }

        public void AddOtp(UserOtp otpValue)
        {
            _context.UserOtps.Add(otpValue);
            _context.SaveChanges();
        }


        public UserOtp GetUserRelatedData(string otpname)
        {
            return _context.UserOtps.Where(a => a.OtpName == otpname).
                Include(x => x.User).FirstOrDefault();
        }



        public User GetUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            return user;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User UserLogin(User user)
        {
            var data = _context.Users.Where(s => s.Username == user.Username && s.Password == user.Password)
                .FirstOrDefault();
            return data;
        }


        // make a method to get  all data through email.
        public User EmailVerification(string email)
        {
            var res = _context.Users.FirstOrDefault(x => x.Email == email);
            return res;
        }

        //upadte pasword
        public void UpdatePassword(User oldp, User newp)
        {
            _context.Entry<User>(oldp).CurrentValues.SetValues(newp);
            _context.SaveChanges();

        }



        // state and country

        public List<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public List<State> GetStates(int countryId)
        {
            var data = _context.States.Where(s => s.CountryId == countryId).ToList();
            return data;
        }
        // update user details 

        public User UpdateUser(User oldData, User newData)
        {
            _context.Entry<User>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }
        public User ChangePassword(User oldData, User newData)
        {
            _context.Entry<User>(oldData).CurrentValues.SetValues(newData);
            _context.SaveChanges();
            return newData;
        }
    }



}
