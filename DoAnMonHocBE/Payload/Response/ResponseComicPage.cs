using DoAnMonHocBE.Payload.DTO;

namespace DoAnMonHocBE.Payload.Response
{
    public class ResponseComicPage
    {
        public IQueryable<DTO_Comic> Comics { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }

    }
}
