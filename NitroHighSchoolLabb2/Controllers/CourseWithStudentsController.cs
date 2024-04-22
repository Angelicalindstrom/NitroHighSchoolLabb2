using Microsoft.AspNetCore.Mvc;
using NitroHighSchoolLabb2.Data;
using NitroHighSchoolLabb2.Models;

namespace NitroHighSchoolLabb2.Controllers
{
    public class CourseWithStudentsController : Controller
    {
        private readonly NitroDbContext _context;

        public CourseWithStudentsController(NitroDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // hämta alla elever som går Programmering 1 och visa deras lärare i den kursen
            // Till den här Controllern gjorde jag en ny hjälpmodel(class) i den listor och Prop kursnamn för att
            // kunna loopa igenom dom sedan i
            // min View (CourseWithStudents) för att hämta
			var query = from cts in _context.CoursesInfo
                        join s in _context.Students on cts.StudentId equals s.StudentId
                        join c in _context.Courses on cts.CourseId equals c.CourseId
                        join t in _context.Teachers on cts.TeacherId equals t.TeacherId
                        group new { s.StudentName, t.TeacherName } by new { c.CourseName, s.StudentName } into coursegroup
                        select new StudentWithTeachers

                        {
                            CourseName = coursegroup.Key.CourseName,
                            StudentNames = coursegroup.Select(s => s.StudentName).ToList(),
                            TeacherNames = coursegroup.Select(t => t.TeacherName).ToList()
                        };

            return View(query.ToList());
        }
    }
}
