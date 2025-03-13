using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interface
{
    public interface IService_Hobby
    {
        public ResponseBase ToggleHobby(int userId, int comicId);
        public IQueryable<DTO_Hobby> GetListHobby(int userId);
        public ResponseBase GetLikeStatus(int userId, int comicId);
    }
}
