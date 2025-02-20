using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.Payload.Request.ComicType;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interface
{
    public interface IService_ComicType
    {
        public ResponseBase CreateComicType(Request_CreateComicType request);
        public List<DTO_ComicType> GetListComicType();
        public ResponseObject<DTO_ComicType> UpdateComicType(Request_UpdateComicType request);
        public ResponseObject<DTO_ComicType> DeleteComicType(int comicTypeId);
    }
}
