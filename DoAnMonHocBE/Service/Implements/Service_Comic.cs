using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Handle;
using DoAnMonHocBE.Payload.Converter;
using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.Payload.Request.Comic;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interface;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_Comic : IService_Comic
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_Comic> responseObject;
        private readonly Converter_Comic converter_Comic;

        public Service_Comic(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_Comic> responseObject, Converter_Comic converter_Comic)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_Comic = converter_Comic;
        }

        public ResponseBase CreateComic(Request_CreateComic request)
        {
            if (dbContext.comics.Any(x => x.ComicName == request.ComicName))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Truyện đã tồn tại");
            }
            if (request.UrlImage == null)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng chọn ảnh bìa cho truyện !");
            }

            if (!CheckInput.IsImage(request.UrlImage))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ !");
            }

            CloudinaryService cloudinaryService = new CloudinaryService();
            string UrlImage = cloudinaryService.UploadImage(request.UrlImage);

            var newComic = new Comic()
            {
                ComicName = request.ComicName,
                ComicContent = request.ComicContent,
                ComicAuthor = request.ComicAuthor,
                CreateDate = request.CreateDate,
                UrlImage = UrlImage,
                ComicTypeId = request.ComicTypeId,
            };

            dbContext.comics.Add(newComic);
            dbContext.SaveChanges();

            return responseBase.ResponseSuccess("Tạo truyện thành công !");
        }

        public ResponseObject<DTO_Comic> DeleteComic(int comicId)
        {
            var comic = dbContext.comics.FirstOrDefault(x => x.Id == comicId);
            if (comic == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy truyện", null);    
            }

            dbContext.comics.Remove(comic);
            dbContext.SaveChanges();

            return responseObject.ResponseObjectSuccess("Xóa truyện thành công !", null);

        }

        public string GetComicContent(int comicId, int pageNumber)
        {
            int pageSize = 400; // Số từ mỗi trang (400-500 từ)

            var comic = dbContext.comics.FirstOrDefault(x => x.Id == comicId);
            if (comic == null)
                return "Không tìm thấy truyện";
            var comicContent = comic.ComicContent;

            if (string.IsNullOrEmpty(comicContent)) 
            {
                return "Không tìm thấy nội dung truyện";
            }

            var words = comicContent.Split(' ');

            int totalPages = (int)Math.Ceiling((double)words.Length / pageSize);

            if (pageNumber < 1 || pageNumber > totalPages) 
            {
                return "Trang không hợp lệ";
            }

            var pageComicContent = words.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return string.Join(" ", pageComicContent);
        }

        public List<DTO_Comic> GetListComic()
        {
            return  dbContext.comics
                .Select(comic => converter_Comic.EntityToDTO(comic))
                .ToList();
        }

        public ResponseObject<DTO_Comic> UpdateComic(Request_UpdateComic request)
        {
            var comic = dbContext.comics.FirstOrDefault(x => x.Id == request.Id);
            if (comic == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy truyện", null);
            }

            // Cập nhật các thuộc tính nếu có giá trị mới, nếu không giữ nguyên giá trị cũ
            comic.ComicName = request.ComicName ?? comic.ComicName;
            comic.ComicContent = request.ComicContent ?? comic.ComicContent;
            comic.ComicAuthor = request.ComicAuthor ?? comic.ComicAuthor;
            comic.ComicTypeId = request.ComicTypeId ?? comic.ComicTypeId;

            // Nếu request có ảnh mới, thì cập nhật ảnh
            CloudinaryService cloudinaryService = new CloudinaryService();
            string urlImage = comic.UrlImage; // Giữ nguyên ảnh cũ nếu không có ảnh mới

            if (request.UrlImage != null)
            {
                if (!CheckInput.IsImage(request.UrlImage))
                {
                    return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ!", null);
                }
                urlImage = cloudinaryService.UploadImage(request.UrlImage);
            }

            comic.UrlImage = urlImage;

            dbContext.comics.Update(comic);
            dbContext.SaveChanges();

            // Chuyển đổi entity sang DTO để trả về response
            var comicDto = converter_Comic.EntityToDTO(comic);

            return responseObject.ResponseObjectSuccess("Cập nhật truyện thành công!", comicDto);
        }

        public ResponseObject<DTO_Comic> UpdateContentbyAdmin(Request_UpdateContentComic request)
        {
            if (request.pageNumber == 0)
                return responseObject.ResponseObjectError(400, "Trang không hợp lệ !", null);
            int pageSize = 400; 

            var comic = dbContext.comics.Find(request.comicId);
            if (comic == null)
            {
                return responseObject.ResponseObjectError(404,"Không tìm thấy truyện!", null);
            }

            var numberConTentBefor = pageSize * (request.pageNumber - 1);

            var words = comic.ComicContent.Split(' ');

            if (numberConTentBefor >= words.Length)
            {
                return responseObject.ResponseObjectError(400,"Trang không hợp lệ!", null);
            }

            // Nội dung trước trang cần cập nhật
            var contentBefor = string.Join(" ", words.Take(numberConTentBefor));

            // Nội dung sau trang cần cập nhật
            var contentAfter = string.Join(" ", words.Skip(numberConTentBefor + pageSize));

            // Nội dung mới ghép lại
            var conTentNew = contentBefor + " " + request.conTent + " " + contentAfter;
            comic.ComicContent = conTentNew.Trim();

            dbContext.Update(comic);
            dbContext.SaveChanges();

            return responseObject.ResponseObjectSuccess("Cập nhật truyện thành công!", null);
        }

    }
}
