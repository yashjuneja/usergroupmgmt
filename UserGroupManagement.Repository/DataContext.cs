using Microsoft.EntityFrameworkCore;
using UserGroupManagement.Repository.Entities;

namespace UserGroupManagement.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Groups)
                .WithMany(g => g.Users);
        }

    }
}
