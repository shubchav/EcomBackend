using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MixedAssessmentEcom.Domain.Models;
using MixedAssessmentEcom.Repository.UserRepository;
using MixedAssessmentEcom.Servise.EncrptionDecryption;
using MixedAssessmentEcom.ViewModels;
using MixedAssessmentEcom.ViewModels.UsersViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MixedAssessmentEcom.Servise.UserServise
{
    public class UserServ : IUserServ
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;

        private static HttpClient _client = new HttpClient();
        
        private readonly IWebHostEnvironment _environment;

        public UserServ(IUserRepo userRepo, IMapper mapper, IConfiguration iconfiguration, IWebHostEnvironment environment)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _iconfiguration = iconfiguration;
            _environment = environment;
        }
        public UserViewVm GetUser(int userId)
        {
            CodeDecode codeDecode = new CodeDecode();

            var userdata = _userRepo.GetUser(userId);
            var updatedUserdata = new UserViewVm()
            {
                UserId = userdata.UserId,
                Firstname = userdata.Firstname,
                Lastname = userdata.Lastname,
                Email = userdata.Email,
                UserType = userdata.UserType,
                DOB = userdata.DOB,
                Username = codeDecode.DecryptString(userdata.Username),
                MobileNumber = userdata.MobileNumber,
                Address = userdata.Address,
                Zipcode = userdata.Zipcode,
                ProfileImage = userdata.ProfileImage,
                Country = userdata.Country,
                State = userdata.State,
            };
            var usr = _mapper.Map<UserViewVm>(userdata);
            return updatedUserdata;
        }


        public async Task<UserViewVm> AddUser(UserRegistrationVM userRegVM)
        {

            CodeDecode codeDecode = new CodeDecode();

            var genUsername = "ES_" + userRegVM.Lastname.ToUpper() + userRegVM.Firstname.Substring(0, 1).ToUpper() + userRegVM.DOB.ToString("ddMMyy");

            String Letters = "ABCDFFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789!@#$%^&*";
            char[] Alphanumeric = (Letters).ToCharArray();
            StringBuilder RandomChar = new System.Text.StringBuilder();
            Random rnd = new Random();
            for (int i = 1; i <= 8; i++)
            {
                RandomChar.Append(Alphanumeric[rnd.Next(Alphanumeric.Length)]);
            }

            var pass = RandomChar.ToString();
            // code for send username and password on user mail.

            // preparing emailbody.
            //var template = await System.IO.File.ReadAllTextAsync(
            //    System.IO.Path.Combine(_environment.WebRootPath, "testtemplate.html")
            //    );

            //replacing name with real name.

            //var body = ("[User]", userRegVM.Firstname);

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Shopping App", _iconfiguration["SMTPGmailEmail"]));
            mailMessage.To.Add(new MailboxAddress("Shubham Chavan", userRegVM.Email));
            mailMessage.Subject = "Welcome email";

            mailMessage.Body = new TextPart("html")
            {
                Text = "Dear Miss/Mr," + userRegVM.Firstname + "\n" + "\n" + "Your Username is : " + genUsername + "\n" + "Your Password is: " + pass
                //Text= @"<html>

                //      <body>

                //      <p>Dear Ms/Mr. {{userRegVM.Firstname}},</p>

                //      <p> Thank you for choosing our Shopping applocation.</p>
                //              <p>Your Username is : {{genUsername }} and Password : {{pass}}</p>

                //      <p>Sincerely,<br>-Shopping App Team</br></p>

                //      </body>

                //      </html>"
            };


            //var builder = new BodyBuilder();
            //// Set the plain-text version of the message text
            //builder.HtmlBody = body;

            //// Availabled attachment
            //builder.Attachments.Add(System.IO.Path.Combine(_environment.WebRootPath, "Attachments", "TestPDFfile.pdf"));

            //// Downloaded attachment.

            //var res = await _client.GetByteArrayAsync("https://www.africau.edu/images/default/sample.pdf");
            //builder.Attachments.Add("secondAttachment.pdf", res);

            //mailMessage.Body = builder.ToMessageBody();


            using (var smtpClient = new SmtpClient())
            {
                smtpClient.CheckCertificateRevocation = false;
                await smtpClient.ConnectAsync(_iconfiguration["GmailHost"], Convert.ToInt16(_iconfiguration["GmailPort"]), false);
                await smtpClient.AuthenticateAsync(_iconfiguration["SMTPGmailEmail"], _iconfiguration["SMTPGmailPassword"]);
                //smtpClient.SslProtocols = 
                await smtpClient.SendAsync(mailMessage);
                await smtpClient.DisconnectAsync(true);
            }






            var userclass = new User()
            {
                UserId = 0,
                Firstname = userRegVM.Firstname,
                Lastname = userRegVM.Lastname,
                Email = userRegVM.Email,
                UserType = userRegVM.UserType,
                DOB = userRegVM.DOB,
                Username = codeDecode.EncryptString(genUsername),
                Password = codeDecode.EncryptString(pass),
                MobileNumber = userRegVM.MobileNumber,
                Address = userRegVM.Address,
                Zipcode = userRegVM.Zipcode,
                ProfileImage = userRegVM.ProfileImage,
                Country = userRegVM.Country,
                State = userRegVM.State,
            };




            _userRepo.AddUser(userclass);

            var userVm = _mapper.Map<UserViewVm>(userclass);

            return userVm;
        }

        public string LoginUser(UserLoginVM loginVm)
        {
            CodeDecode codeDecode = new CodeDecode();

            var dusername = codeDecode.EncryptString(loginVm.Username);
            var dpassword = codeDecode.EncryptString(loginVm.Password);

            var loginuservm = new UserLoginVM()
            {
                Username = dusername,
                Password = dpassword
            };

            //checking condition of username and password
            var loginvm = _mapper.Map<User>(loginuservm);
            var log = _userRepo.UserLogin(loginvm);
            if (log != null)
            {
                // if username and password are correct then then generate otp
                String Letters = "0123456789";
                char[] Alphanumeric = (Letters).ToCharArray();
                StringBuilder RandomChar = new System.Text.StringBuilder();
                Random rnd = new Random();
                for (int i = 1; i <= 6; i++)
                {
                    RandomChar.Append(Alphanumeric[rnd.Next(Alphanumeric.Length)]);
                }
                // date time adding more time in current time for expiry of otp
                DateTime date = DateTime.Now;
                TimeSpan time = new TimeSpan(0, 0, 10, 0);
                DateTime combined = date.Add(time);

                var otpgenerate = RandomChar.ToString();
                var accounsid = this._iconfiguration.GetSection("TwilioAccountDetails")["AccountSid"];
                var authToken = this._iconfiguration.GetSection("TwilioAccountDetails")["AuthToken"];
                TwilioClient.Init(accounsid, authToken);

                var to = "+917083908592";
                var from = "+17174448377";


                var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: "Your One Time Password is:" + otpgenerate + "and Valid for 10 Miniutes"
                // Here's where you inject the custom client

                );

                var otpclass = new UserOtp()
                {
                    OtpId = 0,
                    // otp name is generated otp
                    OtpName = otpgenerate,
                    CreatedTimeOtp = date,
                    ExpireTimeOtp = combined,
                    UserId = log.UserId,
                };

                _userRepo.AddOtp(otpclass);

                // i have write a method for user input otp match with the below method .
                //var userOtp = UserOtpMethod();

                //var generatedToken = TokenValidate(loginVm, otpclass.OtpName);

                return otpgenerate;

            }
            else
            {
                return "User Not Found.";
            }




        }

        //method for otp verification through the user send

        //public string UserOtpVerify(string otpName)
        //{
        //    var d = _userRepo.GetOtp(otpName);

        //    if (d.OtpName == otpName)
        //    {
        //        //return true;
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            //ApplicantId, Username, FullName and Email along with token expiry and other details
        //            Subject = new ClaimsIdentity(new Claim[]
        //                {

        //                    new Claim("Id", log.UserId.ToString()),
        //                    new Claim("UserRole", log.UserType.ToString()),
        //                    new Claim("Username", log.Username),
        //                }),
        //            Expires = DateTime.UtcNow.AddMinutes(30),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        var tokendata = new TokenResponse { Token = tokenHandler.WriteToken(token) };


        //        return tokendata.Token;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}



        public string TokenValidate( string otpName)
        {
            CodeDecode codeDecode = new CodeDecode();
            // method to received the data of otp
            var otpReceived = _userRepo.GetOtp(otpName);
            // method to get the user data 
            var data = _userRepo.GetUserRelatedData(otpName);
            // create a new class for related data
            if(data == null)
            {
                 return "Otp is wrong";
            }
           
                var usertokenData = new UserTokenDataVM()
                {
                    UserId = data.UserId,
                    UserType = data.User.UserType,
                    Username = data.User.Username,
                    Firstname = data.User.Firstname,
                };
            
            
           

           
           
            if (otpReceived.OtpName == otpName)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //ApplicantId, Username, FullName and Email along with token expiry and other details
                    Subject = new ClaimsIdentity(new Claim[]
                        {

                            new Claim("Id", usertokenData.UserId.ToString()),
                            new Claim("UserRole", usertokenData.UserType.ToString()),
                            new Claim("Username", usertokenData.Username),
                        }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokendata = new TokenResponse { Token = tokenHandler.WriteToken(token) };


                return tokendata.Token;
            }
            else
            {
                return "Otp is wrong";

                // return "User Not Found";

            }

        }

        public UserTokenDataVM UserRelatedData(string otpName)
        {
            var data  = _userRepo.GetUserRelatedData(otpName);

            var usertokenData = new UserTokenDataVM()
            {
                UserId = data.UserId,
                UserType = data.User.UserType,
                Username = data.User.Username,
                Firstname = data.User.Firstname,
            };

            return usertokenData;

        }

        // forget password method 

        public async Task<string> UpdatePassword(string email)
        {
            String Letters = "ABCDFFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789!@#$%^&*";
            char[] Alphanumeric = (Letters).ToCharArray();
            StringBuilder RandomChar = new System.Text.StringBuilder();
            Random rnd = new Random();
            for (int i = 1; i <= 8; i++)
            {
                RandomChar.Append(Alphanumeric[rnd.Next(Alphanumeric.Length)]);
            }

            var pass = RandomChar.ToString();
            
           
                
                var olddata = _userRepo.EmailVerification(email);
                if (olddata != null)
                {
                    CodeDecode codeDecode = new CodeDecode();


                    //uservm.Password = pass;
                    var encyPassword = codeDecode.EncryptString(pass);
                    //var newdata = _mapper.Map<EmailVm, User>(uservm, olddata);

                    //response.Response = newdata;

                   // uservm.Password = codeDecode.DecryptString(newdata.Password);

                    var body = "NewPassword:" + pass;

                    //var newP = uservm.Password;
                    

                    var newPassword = new EmailVm()
                    {
                        Password = encyPassword
                    };


                    var userclass = new User()
                    {
                    UserId = olddata.UserId,
                    Firstname = olddata.Firstname,
                    Lastname = olddata.Lastname,
                    Email = olddata.Email,
                    UserType = olddata.UserType,
                    DOB = olddata.DOB,
                    Username = olddata.Username,
                    Password = newPassword.Password,
                    MobileNumber = olddata.MobileNumber,
                    Address = olddata.Address,
                    Zipcode = olddata.Zipcode,
                    ProfileImage = olddata.ProfileImage,
                    Country = olddata.Country,
                    State = olddata.State,
                     };

                    var mapperOfpassword = _mapper.Map<User>(newPassword);

                    _userRepo.UpdatePassword(olddata, userclass);



                    var mailMessage = new MimeMessage();
                    mailMessage.From.Add(new MailboxAddress("Shubham", _iconfiguration["SMTPGmailEmail"]));
                    mailMessage.To.Add(new MailboxAddress(olddata.Firstname, "shubhamchavan272000@gmail.com"));
                    mailMessage.Subject = "New Password";


                    var builder = new BodyBuilder();

                    builder.HtmlBody = body;

                    mailMessage.Body = builder.ToMessageBody();


                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.CheckCertificateRevocation = false;
                        await smtpClient.ConnectAsync(_iconfiguration["GmailHost"], Convert.ToInt16(_iconfiguration["GmailPort"]), false);
                        await smtpClient.AuthenticateAsync(_iconfiguration["SMTPGmailEmail"], _iconfiguration["SMTPGmailPassword"]);
                        //smtpClient.SslProtocols = 
                        await smtpClient.SendAsync(mailMessage);
                        await smtpClient.DisconnectAsync(true);

                    }
                    return "Password Updated";
                }
            else
            {
                return "Email is wrong";

                // return "User Not Found";

            }



        }

        ///// code for city and state    ///

        public List<CountryVM> GetCountries()
        {
            var pol = _userRepo.GetCountries();
            var mpol = _mapper.Map<List<CountryVM>>(pol);

            return mpol;
        }

        public List<StateVM> GetStates(int countryId)
        {
            var pol = _userRepo.GetStates(countryId);
            var mpol = _mapper.Map<List<StateVM>>(pol);
            return mpol;
        }

        // update the user profile 
        public User UpdateUser(int UserId, UserRegistrationVM userRegistrationVM )
        {

            var exitdata = _userRepo.GetUser(UserId);

            var userClass = new User()
            {
                UserId = exitdata.UserId,
                Firstname = userRegistrationVM.Firstname,
                Lastname = userRegistrationVM.Lastname,
                Email = userRegistrationVM.Email,
                UserType = exitdata.UserType,
                DOB = userRegistrationVM.DOB,
                Username = exitdata.Username,
                Password = exitdata.Password,
                MobileNumber = userRegistrationVM.MobileNumber,
                Address = userRegistrationVM.Address,
                Zipcode = userRegistrationVM.Zipcode,
                ProfileImage = userRegistrationVM.ProfileImage,
                Country = userRegistrationVM.Country,
                State = userRegistrationVM.State,

            };

            // data class type
            //var productdata = _mapper.Map<Product>(productVM);

            // repo calling 
            _userRepo.UpdateUser(exitdata, userClass);
            // data of vm type 
            //var userVMDAta = _mapper.Map<UserRegistrationVM>(userClass);

            return userClass;
        }


        public string ChangePassword(int userId , EmailVm emailVm)
        {
           var exitingData = _userRepo.GetUser(userId);
            CodeDecode codeDecode = new CodeDecode();

            var userClass = new User()
            {
                UserId = exitingData.UserId,
                Firstname = exitingData.Firstname,
                Lastname = exitingData.Lastname,
                Email = exitingData.Email,
                UserType = exitingData.UserType,
                DOB = exitingData.DOB,
                Username = exitingData.Username,
                Password = codeDecode.EncryptString(emailVm.Password),
                MobileNumber = exitingData.MobileNumber,
                Address = exitingData.Address,
                Zipcode = exitingData.Zipcode,
                ProfileImage = exitingData.ProfileImage,
                Country = exitingData.Country,
                State = exitingData.State,
            };
            _userRepo.ChangePassword(exitingData, userClass);
            return "Password change Successfully.";
        }

    }
}











