using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Payload.Converter;
using DoAnMonHocBE.Payload.DTO;
using DoAnMonHocBE.Payload.Request.ComicType;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interface;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_ComicType : IService_ComicType
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_ComicType> responseObject;
        private readonly Converter_ComicType converter_ComicType;

        public Service_ComicType(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_ComicType> responseObject, Converter_ComicType converter_ComicType)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_ComicType = converter_ComicType;
        }

        public ResponseBase CreateComicType(Request_CreateComicType request)
        {
            if (dbContext.comictypes.Any(x => x.ComicTypeName == request.ComicTypeName))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Thể loại truyện đã tồn tại");
            }

            var newComicType = new ComicType();

            newComicType.ComicTypeName = request.ComicTypeName;

            dbContext.comictypes.Add(newComicType);
            dbContext.SaveChanges();

            return responseBase.ResponseSuccess("Thêm thể loại truyện thành công");
        }

        public ResponseObject<DTO_ComicType> DeleteComicType(int comicTypeId)
        {
            var comicType = dbContext.comictypes.FirstOrDefault(x => x.Id == comicTypeId);

            if (comicType == null) 
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy thể loại truyện", null);
            }

            // Kiểm tra có truyện nào đang sử dụng ComicTypeId này không
            bool isUsed = dbContext.comics.Any(x => x.ComicTypeId == comicTypeId);
            if (isUsed)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Không thể xóa thể loại vì đang có truyện sử dụng", null);
            }

            dbContext.Remove(comicType);
            dbContext.SaveChanges();

            return responseObject.ResponseObjectSuccess("Xóa thành công!", null);
        }

        public List<DTO_ComicType> GetListComicType()
        {
            return dbContext.comictypes
               .Select(comicType => converter_ComicType.EntityToDTO(comicType))
               .ToList();
        }

        public ResponseObject<DTO_ComicType> UpdateComicType(Request_UpdateComicType request)
        {
            var comicType = dbContext.comictypes.FirstOrDefault(x => x.Id == request.Id);

            if (comicType == null) 
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy thể loại truyện!", null);
            }

            comicType.ComicTypeName = request.ComicTypeName;
            dbContext.Update(comicType);
            dbContext.SaveChanges();

            return responseObject.ResponseObjectSuccess("Sửa tên thể loại thành công!", null);
        }
    }
}
