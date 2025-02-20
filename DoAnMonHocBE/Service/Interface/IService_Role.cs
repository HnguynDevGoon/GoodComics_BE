using DoAnMonHocBE.PayLoad.DTO;
using DoAnMonHocBE.PayLoad.Request.Role;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interfaces
{
    public interface IService_Role
    {
        public ResponseBase CreateRole(Request_CreateRole request);
        public ResponseObject<DTO_Role> UpdateRole(Request_UpdateRole request);
        public IQueryable<DTO_Role> GetListRole(int pageSize, int pageNumber);
        public ResponseObject<DTO_Role> GetRoleById(int roleId);
        public ResponseObject<DTO_Role> DeleteRole(int roleId);
    }
}
