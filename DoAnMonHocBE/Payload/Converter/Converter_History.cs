using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHocBE.Payload.Converter
{
    public class Converter_History
    {
       private readonly AppDbContext dbContext;

        public Converter_History(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DTO_History EntityToDTO(History history)
        {
            var user = dbContext.users.Find(history.UserId);
            var comic = dbContext.comics
            .Include(c => c.ComicType) // Nạp ComicType luôn
            .FirstOrDefault(c => c.Id == history.ComicId);

            return new DTO_History
            {
                Id = history.Id,
                LastRead = new DateTime(
                history.LastRead.Year,
                history.LastRead.Month,
                history.LastRead.Day,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second
            ),
                ComicId = history.ComicId,
                UserId = history.UserId,
                Username = user.Username,
                ComicName = comic.ComicName,
                UrlImg = comic.UrlImage,
                ComicAuthor = comic.ComicAuthor,
                ComicTypeName = comic.ComicType?.ComicTypeName ?? "Không xác định"
            };
        }
    }
}