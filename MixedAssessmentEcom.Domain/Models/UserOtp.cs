using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixedAssessmentEcom.Domain.Models
{
    public class UserOtp
    {
        public int OtpId { get; set; }
        public string OtpName { get; set; }
        public DateTime CreatedTimeOtp { get; set; }
        public DateTime ExpireTimeOtp { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
