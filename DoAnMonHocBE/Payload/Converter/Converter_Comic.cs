using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;

namespace DoAnMonHocBE.Payload.Converter
{
    public class Converter_Comic
    {
        private readonly AppDbContext dbContext;

        public Converter_Comic(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DTO_Comic EntityToDTO(Comic comic)
        {
            var comictype = dbContext.comictypes.Find(comic.ComicTypeId);

            return new DTO_Comic
            {
                Id = comic.Id,
                ComicName = comic.ComicName,
                ComicAuthor = comic.ComicAuthor,
                ComicContent = comic.ComicContent,
                CreateDate = comic.CreateDate,
                UrlImage = comic.UrlImage,
                ComicTypeId = comic.ComicTypeId,
                ComicTypeName=comictype.ComicTypeName,
            };
        }
    }
}
