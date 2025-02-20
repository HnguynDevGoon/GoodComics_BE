using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;

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
            var comic = dbContext.comics.Find(history.ComicId);

            return new DTO_History
            {
                Id = history.Id,
                LastRead = history.LastRead,
                ComicId = history.ComicId,
                UserId = history.UserId,
                Username = user.Username,
                ComicName = comic.ComicName,
            };
        }
    }
}
