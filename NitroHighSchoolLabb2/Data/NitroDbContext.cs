using Microsoft.EntityFrameworkCore;
using NitroHighSchoolLabb2.Models;

namespace NitroHighSchoolLabb2.Data
{
    public class NitroDbContext : DbContext
    {
        public NitroDbContext(DbContextOptions<NitroDbContext> options )
        : base (options ) 
        { 
        }

        public DbSet <Student> Students { get; set; }
        public DbSet <StudentClass> StudentClasses { get; set; }
        public DbSet <Teacher> Teachers { get; set; }
        public DbSet <Course> Courses { get; set; }
        public DbSet <CourseInfo> CoursesInfo { get; set;}
    }
}
