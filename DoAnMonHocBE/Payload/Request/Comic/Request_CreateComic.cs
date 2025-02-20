
namespace DoAnMonHocBE.Payload.Request.Comic
{
    public class Request_CreateComic
    {
        public string ComicName { get; set; }
        public string ComicContent { get; set; }
        public string ComicAuthor { get; set; }
        public DateTime CreateDate { get; set; }
        public IFormFile UrlImage { get; set; }
        public int ComicTypeId { get; set; }
    }
}
