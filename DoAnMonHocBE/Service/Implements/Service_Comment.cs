using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.Converter;
using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interface;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_Comment : IService_Comment
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_Comment> responseObject;
        private readonly Converter_Comment converter_Comment;

        public Service_Comment(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_Comment> responseObject, Converter_Comment converter_Comment)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_Comment = converter_Comment;
        }

        public ResponseBase AddNewComment(string commentTitle, int rate, int userId, int comicId)
        {

            if (!dbContext.comics.Any(x => x.Id == comicId))
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Truyện không tồn tại");
            }

            if (rate < 1 || rate > 5)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Đánh giá phải từ 1 đến 5");
            }

            var newComment = new Comment
            {
                CommentTitle = commentTitle,
                Rate = rate,
                UserId = userId,
                ComicId = comicId,
            };

            dbContext.comments.Add(newComment);
            dbContext.SaveChanges();

            return responseBase.ResponseSuccess("Đã thêm bình luận thành công!");
        }

        public IQueryable<DTO_Comment> GetListComment(int comicId)
        {
            return dbContext.comments
            .Where(c => c.ComicId == comicId) // Lọc bình luận theo comicId
            .OrderByDescending(c => c.Id)
            .Select(c => converter_Comment.EntityToDTO(c));
        }


    }
}
