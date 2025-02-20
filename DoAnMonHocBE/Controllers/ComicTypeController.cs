using DoAnMonHocBE.Payload.Request.ComicType;
using DoAnMonHocBE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicTypeController : ControllerBase
    {
        private readonly IService_ComicType service_ComicType;
        public ComicTypeController(IService_ComicType service_ComicType)
        {
            this.service_ComicType = service_ComicType;
        }

        [HttpPost("CreateComicType")]
        public IActionResult CreateComicType(Request_CreateComicType request)
        {
            return Ok(service_ComicType.CreateComicType(request));
        }

        [HttpGet("GetListComicType")]
        public IActionResult GetListComicType()
        {
            return Ok(service_ComicType.GetListComicType());
        }

        [HttpPut("UpdateComicType")]
        public IActionResult UpdateComicType(Request_UpdateComicType request) 
        {
            return Ok(service_ComicType.UpdateComicType(request));
        }

        [HttpDelete("DeleteComicType")]
        public IActionResult DeleteComicType(int comicTypeId) 
        {
            return Ok(service_ComicType.DeleteComicType(comicTypeId));
        }



    }
}
