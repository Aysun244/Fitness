using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using Fitness.ViewModels.CourseVM;

namespace Fitness.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<Course>Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Fitness.ViewModels.CourseVM.CourseGetVM> CourseGetVM { get; set; } = default!;
      
    }
}
