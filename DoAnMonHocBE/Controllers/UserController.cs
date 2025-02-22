using DoAnMonHocBE.PayLoad.Request.User;
using DoAnMonHocBE.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService_User service_User;

        public UserController(IService_User service_User)
        {
            this.service_User = service_User;
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(Request_CreateUser? request)
        {
            return Ok(service_User.CreateUser(request));
        }

        [HttpGet("GetListUser")]
        public IActionResult GetListUser(int pageSize = 10, int pageNumber = 1)
        {
            return Ok(service_User.GetListUser(pageSize, pageNumber));
        }

        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            return Ok(service_User.GetUserById(userId));
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int userId)
        {
            return Ok(service_User.DeleteUser(userId));
        }

        [HttpPut("UpdateAvatar")]
        public IActionResult UpdateAvatar(Request_UpdateUser? request)
        {
            return Ok(service_User.UpdateAvatar(request));
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(int userId, string oldPass, string newPass)
        {
            return Ok(service_User.ChangePassword(userId, oldPass, newPass));
        }

        //[HttpPut("AccountVerification")]
        //public IActionResult AccountVerification(string code)
        //{
        //    return Ok(service_User.AccountVerification(code));
        //}

        [HttpPost("UserLogin")]
        public IActionResult UserLogin(Request_Login request)
        {
            return Ok(service_User.UserLogin(request));
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email) 
        {
            return Ok(service_User.ForgotPassword(email));
        }

        [HttpPost("CheckOtp")]
        public IActionResult CheckOtp(int otp) 
        {
            return Ok(service_User.CheckOtp(otp));
        }

        [HttpPut("UpdatePassAfterOtp")]
        public IActionResult UpdatePassAfterOtp(int userId, string newPass, string confirmPass)
        {
            return Ok(service_User.UpdatePassAfterOtp(userId, newPass, confirmPass));
        }

    }
}
