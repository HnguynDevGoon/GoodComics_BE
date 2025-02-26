using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.Payload.Request.Comic;
using DoAnMonHocBE.Payload.Response;
using DoAnMonHocBE.PayLoad.DTO;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interface
{
    public interface IService_Comic
    {
        public ResponseBase CreateComic(Request_CreateComic request);
        public ResponseObject<DTO_Comic> UpdateComic(Request_UpdateComic request);
        public ResponseObject<DTO_Comic> UpdateContentbyAdmin(Request_UpdateContentComic request);
        public ResponseObject<DTO_Comic> DeleteComic(int comicId);
        public ResponseComicPage GetListComic(int pageNumber, int pageSize);
        public ComicContentResponse GetComicContent(int comicId, int pageNumber);
        public ResponseObject<DTO_Comic> GetComicById(int comicId);

    }
}
