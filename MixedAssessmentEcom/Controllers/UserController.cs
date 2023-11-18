   using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixedAssessmentEcom.Servise.UserServise;
using MixedAssessmentEcom.ViewModels.UsersViewModels;

namespace MixedAssessmentEcom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServ _userServ;

        public UserController(IUserServ userServ)
        {
            _userServ = userServ;
        }


        //  [Authorize]
        [HttpGet]
        public IActionResult GetUser(int userId)
        {
            if (userId == 0 || userId == null)
            {
                return BadRequest("Invalid or null input.");
            }
            var user = _userServ.GetUser(userId);
            return Ok(user);
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationVM value)
        {
            if (value == null)
            {
                return BadRequest("");
            }

            var output = await _userServ.AddUser(value);
            return (Ok(output));
        }


        // [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginVM loginVM)
        {


            if (loginVM.Username == null || loginVM.Password == null || loginVM.Username == "" || loginVM.Password == "")
                return BadRequest("User not found");


            var response = _userServ.LoginUser(loginVM);
            return Ok(response);


        }

        [HttpPost("otpName")]
        public IActionResult TokenGenerate(string otpName)
        {
            if(otpName == null || otpName == "")
            {
                return BadRequest("Inavalid otp");
            }
            var data = _userServ.TokenValidate(otpName);
            return Ok(data);
        }

        //[HttpGet("otpName")]
        //public IActionResult getAllData(string otpName)
        //{
        //  var  data = _userServ.UserRelatedData(otpName);
        //    return Ok(data);

        //}

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdatedPassword(string email, [FromBody] EmailVm val)
        {

            var respopnse = await _userServ.UpdatePassword(email);
            return Ok(respopnse);
        }

        ///// State and country enpoint ////
        ///
        // for countries
        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            var countries = _userServ.GetCountries();
            if (countries == null)
            {
                return NotFound("Data Not Found.");
            }

            return Ok(countries);

        }

        // for State 

        [HttpGet("states/{countryId}")]
        public IActionResult GetStates(int countryId)
        {
            if (countryId == null || countryId == 0)
            {
                return BadRequest("Please provide valid Id.");
            }

            var states = _userServ.GetStates(countryId);

            if (states == null)
            {
                return NotFound("Data not found.");
            }

            return Ok(states);
        }

        [HttpPut]
        public IActionResult UpdateUser(int userId, [FromBody] UserRegistrationVM UserVm)
        {
            if (userId == 0 || userId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_userServ.UpdateUser(userId, UserVm));
            return Ok(produtUp);
        }


        [HttpPut("changePassword")]
        public IActionResult ChangePassword(int userId, [FromBody] EmailVm UserVm)
        {
            if (userId == 0 || userId == null)
            {
                return NotFound("Id not found.");
            }
            var produtUp = (_userServ.ChangePassword(userId, UserVm));
            return Ok(produtUp);
        }



    }
}
