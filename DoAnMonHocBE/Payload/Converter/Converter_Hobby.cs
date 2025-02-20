using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;

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
            var comic = dbContext.comics.Find(hobby.ComicId);

            return new DTO_Hobby
            {
                Id = hobby.Id,
                ComicId = hobby.ComicId,
                UserId = hobby.UserId,
                ComicName = comic.ComicName,
                Username = user.Username,
            };
        }
    }
}
