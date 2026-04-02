
using login_register_react_app.Models;
using Microsoft.EntityFrameworkCore;

namespace login_register_react_app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
