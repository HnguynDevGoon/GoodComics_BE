using DoAnMonHocBE.Payload.Request.Comic;
using DoAnMonHocBE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicController : ControllerBase
    {
        private IService_Comic service_Comic;

        public ComicController(IService_Comic service_Comic)
        {
            this.service_Comic = service_Comic;
        }

        [HttpPost("CreateComic")]
        public IActionResult CreateComic([FromForm]Request_CreateComic request)
        {
            return Ok(service_Comic.CreateComic(request));
        }

        [HttpPut("UpdateComic")]
        public IActionResult UpdateComic([FromForm]Request_UpdateComic request) 
        {
            return Ok(service_Comic.UpdateComic(request));
        }
        [HttpPut("UpdateContentbyAdmin")]
        public IActionResult UpdateContentbyAdmin([FromForm]Request_UpdateContentComic request)
        {
            return Ok(service_Comic.UpdateContentbyAdmin(request));
        }

        [HttpDelete("DeleteComic")]
        public IActionResult DeleteComic(int comicId)
        {
            return Ok(service_Comic.DeleteComic(comicId));  
        }

        [HttpGet("GetListComic")]
        public IActionResult GetListComic(int pageNumber, int pageSize)
        {
            return Ok( service_Comic.GetListComic(pageNumber, pageSize));
        }

        [HttpGet("GetComicContent")]
        public IActionResult GetComicContent(int comicId, int pageNumber)
        {
            return Ok(service_Comic.GetComicContent(comicId, pageNumber));
        }

        [HttpGet("GetComicById")]
        public IActionResult GetComicById(int comicId)
        { 
            return Ok(service_Comic.GetComicById(comicId));
        }

        [HttpGet("SearchComicByName")]
        public IActionResult SearchComicByName(string comicName)
        {
            return Ok(service_Comic.SearchComicByName(comicName));
        }

        [HttpGet("SearchComicByType")]
        public IActionResult SearchComicByType(string comicTypeName)
        {
            return Ok(service_Comic.SearchComicByType(comicTypeName));
        }

    }
}
