using DoAnMonHocBE.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonHocBE.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<ConfirmEmail> confirmemails { get; set; }
        public DbSet<Comic> comics { get; set; }
        public DbSet<ComicType> comictypes {  get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<History> histories { get; set; }
        public DbSet<Hobby> hobbies { get; set; }
    }
}
