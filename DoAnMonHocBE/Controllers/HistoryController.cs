using DoAnMonHocBE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonHocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IService_History service_History;

        public HistoryController(IService_History service_History)
        {
            this.service_History = service_History;
        }

        [HttpPost("AddHistoryAfterClick")]
        public void AddHistoryAfterClick(int userId, int comicId)
        {
            service_History.AddHistoryAfterClick(userId, comicId);
        }

        [HttpGet("GetListHistory")]
        public IActionResult GetListHistory(int userId)
        { 
            return Ok(service_History.GetListHistory(userId));
        }

    }
}
