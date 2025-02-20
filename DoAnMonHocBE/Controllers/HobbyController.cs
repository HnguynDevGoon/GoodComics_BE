using DoAnMonHocBE.Service.Implements;
using DoAnMonHocBE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly IService_Hobby service_Hobby;

        public HobbyController(IService_Hobby service_Hobby)
        {
            this.service_Hobby = service_Hobby;
        }

        [HttpPost("ToggleHobby")]
        public IActionResult ToggleHobby(int userId, int comicId)
        {
            return Ok(service_Hobby.ToggleHobby(userId, comicId));
        }

        [HttpGet("GetListHobby")]
        public IActionResult GetListHobby(int userId) 
        {
            return Ok(service_Hobby.GetListHobby(userId));
        }
    }
}
