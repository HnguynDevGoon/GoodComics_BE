using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interface
{
    public interface IService_Comment
    {
        public ResponseBase AddNewComment(string commentTitle, int rate, int userId, int comicId);
        public IQueryable<DTO_Comment> GetListComment(int pageSize, int pageNumber);
    }
}
