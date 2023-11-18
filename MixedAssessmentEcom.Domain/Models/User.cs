using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public  string Lastname { get; set; }
        public string Email { get; set; }
        
        public string UserType { get; set; }
        public DateTime DOB { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string ProfileImage { get; set; }
        public string  Country { get; set; }
        public string State { get; set; }

        public List<UserOtp>  Otp { get; set; }


    }
}
//Register and Login module: Create a user module where user with following details Firstname, LastName,
//Email, User type, DOB, Username, Password, mobile, Address, Zipcode(6 digit integer), ProfileImage, State,
//Country(State and Country would be master tables and PK(Id) should be saved in user table instead of text)