using DoAnMonHocBE.Entities;
using DoAnMonHocBE.PayLoad.DTO;

namespace DoAnMonHocBE.PayLoad.Converter
{
    public class Converter_User
    {
        public DTO_User EntityToDTO(User user)
        {
            return new DTO_User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Urlavartar = user.Urlavartar,
            };
        }
    }
}
