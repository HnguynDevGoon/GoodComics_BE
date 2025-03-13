using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interface
{
    public interface IService_History
    {
        public void AddHistoryAfterClick(int userId, int comicId);

        public IQueryable<DTO_History> GetListHistory(int userId);
    }
}
