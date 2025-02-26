using DoAnMonHocBE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IService_Comment service_Comment;

        public CommentController(IService_Comment service_Comment)
        {
            this.service_Comment = service_Comment;
        }

        [HttpPost("AddNewComment")]
        public IActionResult AddNewComment(string commentTitle, int rate, int userId, int comicId)
        {
            return Ok(service_Comment.AddNewComment(commentTitle, rate, userId, comicId));
        }

        [HttpGet("GetListComment")]
        public IActionResult GetListComment(int comicId)
        {
            return Ok(service_Comment.GetListComment(comicId));
        }
    }
}
