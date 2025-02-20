using DoAnMonHocBE.Entities;
using DoAnMonHocBE.PayLoad.DTO;

namespace DoAnMonHocBE.PayLoad.Converter
{
    public class Converter_Role
    {
        public DTO_Role EntityToDTO(Role role)
        {
            return new DTO_Role
            {
                Id = role.Id,
                RoleName = role.Rolename
            };
        }
    }
}
