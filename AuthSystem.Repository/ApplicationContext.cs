using AuthSystem.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Repository
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Filename=AuthSystem.db");
    }
}
