using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;

namespace DoAnMonHocBE.Payload.Converter
{
    public class Converter_Comment 
    {
        private readonly AppDbContext dbContext;

        public Converter_Comment(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DTO_Comment EntityToDTO(Comment comment)
        {
            var user = dbContext.users.Find(comment.UserId);
            return new DTO_Comment 
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                Rate = comment.Rate,
                Username = user.Username,
                UrlAvatar = user.Urlavartar,
            };
        }
    }
}
