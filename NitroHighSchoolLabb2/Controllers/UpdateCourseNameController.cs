using Microsoft.AspNetCore.Mvc;
using NitroHighSchoolLabb2.Data;

namespace NitroHighSchoolLabb2.Controllers
{
    public class UpdateCourseNameController : Controller
    {
        private readonly NitroDbContext _context;

        public UpdateCourseNameController(NitroDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateCourseName()
        {
            var query = from cts in _context.CoursesInfo
                        join c in _context.Courses on cts.CourseId equals c.CourseId
                        where c.CourseName == "Programmering 2"
                        select c;
            // hämtar CourseName och sparar i stringvariabel courseToupDate
            var courseToUpdate = query.FirstOrDefault();

            // OM EJ värde funnet (null)
            if (courseToUpdate == null)
            {
                return NotFound("Course not found");
            }

            courseToUpdate.CourseName = "OOP";
            // Uppdaterar och sparar ändringar
            _context.SaveChanges();

            // OM ändring OK visas meddelande
            return Ok("Course name has been updated to: OOP");
        }
    }
}
