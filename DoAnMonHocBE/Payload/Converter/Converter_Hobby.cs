using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHocBE.Payload.Converter
{
    public class Converter_Hobby
    {
        private readonly AppDbContext dbContext;

        public Converter_Hobby(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public DTO_Hobby EntityToDTO(Hobby hobby)
        {
            var user = dbContext.users.Find(hobby.UserId);
            var comic = dbContext.comics
            .Include(c => c.ComicType) // Nạp ComicType luôn
            .FirstOrDefault(c => c.Id == hobby.ComicId);

            return new DTO_Hobby
            {
                Id = hobby.Id,
                ComicId = hobby.ComicId,
                UserId = hobby.UserId,
                Username = user.Username,
                ComicName = comic.ComicName,
                UrlImg = comic.UrlImage,
                ComicAuthor = comic.ComicAuthor,
                ComicTypeName = comic.ComicType?.ComicTypeName ?? "Không xác định"
            };
        }
    }
}
