using API_Nox.Data.Map;
using API_Nox.Model;
using API_Nox.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API_Nox.Data
{
    public class NoxDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "dbnox_teste");
        }

        public DbSet<UserViewModel> Users { get; set; }
        public DbSet<TaskViewModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TaskMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
