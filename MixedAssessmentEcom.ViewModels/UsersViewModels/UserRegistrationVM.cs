using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.ViewModels.UsersViewModels
{
    public class UserRegistrationVM
    {
       
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public DateTime DOB { get; set; } 
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string ProfileImage { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}
