using Contracts.StorageContract.dbModels;
using Microsoft.EntityFrameworkCore;

namespace DataBaseImplements
{
    public class DataBaseImplement : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=WIN-LT0QI9Q7TRU\SQLEXPRESS;Initial Catalog=CoursWorkDenchik;Integrated Security=True;MultipleActiveResultSets=True;;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
