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
        public ResponseBase AddNewComment(string commentTitle, int rate, int userId, int comicId)
        {
            if (!dbContext.users.Any(x => x.Id == userId))
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại");
            }

            if (!dbContext.comics.Any(x => x.Id == comicId))
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Truyện không tồn tại");
            }

            if (string.IsNullOrWhiteSpace(commentTitle))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Bình luận không được để trống");
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

        public IQueryable<DTO_Comment> GetListComment(int pageSize, int pageNumber)
        {
            return dbContext.comments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => converter_Comment.EntityToDTO(c));
        }

    }
}
