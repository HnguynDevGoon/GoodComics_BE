using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.Converter;
using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interface;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_History : IService_History
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_History> responseObject;
        private readonly Converter_History converter_History;

        public Service_History(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_History> responseObject, Converter_History converter_History)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_History = converter_History;
        }

        public void AddHistoryAfterClick(int userId, int comicId)
        {
            var history = dbContext.histories.FirstOrDefault(x => x.UserId == userId && x.ComicId == comicId);

            if (history == null)
            {
                history = new History
                {
                    UserId = userId,
                    ComicId = comicId,
                    LastRead = DateTime.Now
                };
                dbContext.histories.Add(history);
            }
            else
            {
                history.LastRead = DateTime.Now;
                dbContext.histories.Update(history);
            }

            dbContext.SaveChanges();
        }

        public IQueryable<DTO_History> GetListHistory(int userId)
        {
            var histories = dbContext.histories
                .Where(history => history.UserId == userId) // Lọc theo userId
                .OrderByDescending(history => history.LastRead) // Sắp xếp theo LastRead, mới nhất lên trước
                .Select(history => converter_History.EntityToDTO(history));

            return histories;
        }

    }
}
