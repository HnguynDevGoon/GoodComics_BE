using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.PayLoad.DTO;

namespace DoAnMonHocBE.PayLoad.Converter
{
    public class Converter_User
    {
        private readonly AppDbContext dbContext;

        public Converter_User(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DTO_User EntityToDTO(User user)
        {
            var role = dbContext.roles.Find(user.RoleId);
            return new DTO_User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Urlavartar = user.Urlavartar,
                RoleId = role.Id,
            };
        }
    }
}
