using Microsoft.AspNetCore.Mvc;
using NitroHighSchoolLabb2.Data;

namespace NitroHighSchoolLabb2.Controllers
{
    public class ChangeStudentTeacherController : Controller
    {
        private readonly NitroDbContext _context;

        public ChangeStudentTeacherController(NitroDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // uppdatera EN elevs lärare i Programmerings 1
        // hämta lärare och kurs Namn i Deras tabeller,
        // sedan jämföra Id, då CoursesInfo endast har Id
        [HttpPost]
        public IActionResult UpdateTeacher()
        {
            // Hämta Id  Reidar Nielssen
            var reidarNielssenId = _context.Teachers
                                           .Where(t => t.TeacherName == "Reidar Nielssen")
                                           .Select(t => t.TeacherId)
                                           .FirstOrDefault();

            // Hämta Id Programmering 1
            var programmering1Id = _context.Courses
                                           .Where(c => c.CourseName == "Programmering 1")
                                           .Select(c => c.CourseId)
                                           .FirstOrDefault();

            // OM värdena är något annat än Noll
            if (reidarNielssenId != 0 && programmering1Id != 0)
            {
                // Hämta första matchande courseInfo med Reidar Nielssen och Programmering 1
                var updatedCourseInfo = _context.CoursesInfo
                                                 .Where(cts => cts.TeacherId == reidarNielssenId && cts.CourseId == programmering1Id)
                                                 .FirstOrDefault();

                // OM annat än Null värde
                if (updatedCourseInfo != null)
                {
                    // Hämta Id för Tobias Landen
                    var tobiasId = _context.Teachers
                                           .Where(t => t.TeacherName == "Tobias Landen")
                                           .Select(t => t.TeacherId)
                                           .FirstOrDefault();
                    // Om Tobias Id är annat än Null
                    if (tobiasId != 0)
                    {
                        // Uppdatera lärar-ID för kursen
                        updatedCourseInfo.TeacherId = tobiasId;
                        _context.SaveChanges();
                        return Ok("Teacher updated successfully to: Tobias");
                    }
                    else
                    {
                        return NotFound("New teacher Tobias Landen not found");
                    }
                }
                else
                {
                    return NotFound("Course info not found for Reidar Nielssen in Programmering 1");
                }
            }
            else
            {
                return NotFound("Reidar Nielssen or Programmering 1 not found");
            }
        }


    }
}
