using firstapiproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace firstapiproject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }

    }

}