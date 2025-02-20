using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.DTO;

namespace DoAnMonHocBE.Payload.Converter
{
    public class Converter_Comment 
    {
        public DTO_Comment EntityToDTO(Comment comment)
        {
            return new DTO_Comment 
            {
                Id = comment.Id,
                CommentTitle = comment.CommentTitle,
                Rate = comment.Rate,
            };
        }
    }
}
