using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.Converter;
using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interface;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_Hobby : IService_Hobby
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_Hobby> responseObject;
        private readonly Converter_Hobby converter_Hobby;

        public Service_Hobby(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_Hobby> responseObject, Converter_Hobby converter_Hobby)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_Hobby = converter_Hobby;
        }

        public IQueryable<DTO_Hobby> GetListHobby(int userId)
        {
            return dbContext.hobbies
                .Where(x => x.UserId == userId)
                .Select(x => new DTO_Hobby
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    ComicId = x.ComicId
                });
        }



        public ResponseBase ToggleHobby(int userId, int comicId)
        {
            if (!dbContext.users.Any(x => x.Id == userId))
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại");
            }

            if (!dbContext.comics.Any(x => x.Id == comicId))
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Truyện không tồn tại");
            }

            var hobby = dbContext.hobbies.FirstOrDefault(x => x.UserId == userId && x.ComicId == comicId);

            if (hobby == null)
            {
                dbContext.hobbies.Add(new Hobby { ComicId = comicId, UserId = userId });
                dbContext.SaveChanges();
                return responseBase.ResponseSuccess("Đã thêm vào danh sách yêu thích!");
            }

            dbContext.hobbies.Remove(hobby);
            dbContext.SaveChanges();
            return responseBase.ResponseSuccess("Đã xóa khỏi danh sách yêu thích!");
        }

    }
}
