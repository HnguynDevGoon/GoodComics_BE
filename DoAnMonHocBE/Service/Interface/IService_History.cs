using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interface
{
    public interface IService_History
    {
        public void AddHistoryAfterClick(int userId, int comicId);
    }
}
