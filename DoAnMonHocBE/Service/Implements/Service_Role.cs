using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.PayLoad.Converter;
using DoAnMonHocBE.PayLoad.DTO;
using DoAnMonHocBE.PayLoad.Request.Role;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interfaces;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_Role : IService_Role
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_Role> responseObject;
        private readonly Converter_Role converter_Role;

        public Service_Role(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_Role> responseObject, Converter_Role converter_Role)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_Role = converter_Role;
        }

        public ResponseBase CreateRole(Request_CreateRole request)
        {
            var role = new Role()
            {
                Rolename = request.RoleName,
            };
            /* role.Rolename=request.RoleName;*/
            dbContext.roles.Add(role);
            dbContext.SaveChanges();

            return responseBase.ResponseSuccess("Thêm quyền thành công !");

        }

        public ResponseObject<DTO_Role> DeleteRole(int roleId)
        {
            var role = dbContext.roles.FirstOrDefault(r => r.Id == roleId);
            if (role == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Role không tồn tại.", null);
            }
            dbContext.roles.Remove(role);
            dbContext.SaveChanges();
            return responseObject.ResponseObjectSuccess("Xóa thành công !", null);
        }

        public IQueryable<DTO_Role> GetListRole(int pageSize, int pageNumber)
        {
            return dbContext.roles.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter_Role.EntityToDTO(x));
        }

        public ResponseObject<DTO_Role> GetRoleById(int roleId)
        {
            // Tìm role theo Id
            var role = dbContext.roles.FirstOrDefault(r => r.Id == roleId);

            if (role == null)
            {
                // Trả về lỗi nếu không tìm thấy role
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Role không tồn tại.", null);
            }

            // Chuyển đổi role entity thành DTO
            var roleDto = converter_Role.EntityToDTO(role);

            // Trả về thành công
            return responseObject.ResponseObjectSuccess("Lấy thông tin role thành công.", roleDto);

        }


        public ResponseObject<DTO_Role> UpdateRole(Request_UpdateRole request)
        {
            var role = dbContext.roles.FirstOrDefault(x => x.Id == request.Id);
            if (role == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy !", null);
            }
            role.Rolename = request.RoleName;
            dbContext.roles.Update(role);
            dbContext.SaveChanges();



            return responseObject.ResponseObjectSuccess("Sửa thành công !", converter_Role.EntityToDTO(role));


        }
    }
}
