using Microsoft.EntityFrameworkCore;

namespace EFProfiler.Model
{
    public class EFContext : DbContext
    {
        public DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=.;Initial Catalog=iwangfeng7;User ID=sa;Password=123456");
        }
    }
}