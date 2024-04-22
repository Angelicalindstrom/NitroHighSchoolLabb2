using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NitroHighSchoolLabb2.Data;
using NitroHighSchoolLabb2.Models;

namespace NitroHighSchoolLabb2.Controllers
{
    public class CourseInfoesController : Controller
    {
        private readonly NitroDbContext _context;

        public CourseInfoesController(NitroDbContext context)
        {
            _context = context;
        }

        // GET: CourseInfoes
        public async Task<IActionResult> Index()
        {
            var nitroDbContext = _context.CoursesInfo.Include(c => c.Course).Include(c => c.Student).Include(c => c.Teacher);
            return View(await nitroDbContext.ToListAsync());
        }

        //********************************************************************//
        // HÄMTA ALLA LÄRARE UTEFTER KURSNAMN ( Programmering 1,2,3,OOP )
        // Med hjälp av val på kursnamn i en lista iCourseInfo Index
        // man kan klicka på knappen Get Teachers in Course så skcias man till en ny View ( GetTeachers i CourseInfoes View)
        public IActionResult GetTeachers(string subject)
        {
            var teachers = (from tc in _context.CoursesInfo
                            join t in _context.Teachers on tc.TeacherId equals t.TeacherId
                            join c in _context.Courses on tc.CourseId equals c.CourseId
                            where c.CourseName == subject
                            select t.TeacherName).ToList();

            // Skicka listan med lärarnas namn till vyn
            return View("GetTeachers", teachers);
        }

        //********************************************************************//
        // HÄMTA ALLA ELEVER MED DERAS LÄRARE, SKRIV UT NAMN PÅ ELEV OCH LÄRARE ( FINNS DOM I COURSEINFO DB )
        public IActionResult StudentTeacher()
        {
            var studentTeachers = (from tsc in _context.CoursesInfo
                                   join t in _context.Teachers on tsc.TeacherId equals t.TeacherId
                                   join s in _context.Students on tsc.StudentId equals s.StudentId
                                   join c in _context.Courses on tsc.CourseId equals c.CourseId
                                   select new { StudentName = s.StudentName, TeacherName = t.TeacherName })
                                  .GroupBy(x => x.StudentName)
                                  .ToList();

            return View("StudentTeacher", studentTeachers);
        }


        // GET: CourseInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseInfo = await _context.CoursesInfo
                .Include(c => c.Course)
                .Include(c => c.Student)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.CourseInfoId == id);
            if (courseInfo == null)
            {
                return NotFound();
            }

            return View(courseInfo);
        }

        // GET: CourseInfoes/Create
        public IActionResult Create()
        {
            // visar namn istället för Id i CreateListorna
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName");

            // Skapar en SelectList för Status enum
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(Status)));

            return View();
        }

        // POST: CourseInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseInfoId,Status,CourseId,StudentId,TeacherId")] CourseInfo courseInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseInfo.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", courseInfo.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", courseInfo.TeacherId);
            return View(courseInfo);
        }

        // GET: CourseInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseInfo = await _context.CoursesInfo.FindAsync(id);
            if (courseInfo == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseName", courseInfo.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", courseInfo.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", courseInfo.TeacherId);
            return View(courseInfo);
        }

        // POST: CourseInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseInfoId,Status,CourseId,StudentId,TeacherId")] CourseInfo courseInfo)
        {
            if (id != courseInfo.CourseInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseInfoExists(courseInfo.CourseInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", courseInfo.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", courseInfo.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", courseInfo.TeacherId);
            return View(courseInfo);
        }

        // GET: CourseInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseInfo = await _context.CoursesInfo
                .Include(c => c.Course)
                .Include(c => c.Student)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.CourseInfoId == id);
            if (courseInfo == null)
            {
                return NotFound();
            }

            return View(courseInfo);
        }

        // POST: CourseInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseInfo = await _context.CoursesInfo.FindAsync(id);
            if (courseInfo != null)
            {
                _context.CoursesInfo.Remove(courseInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseInfoExists(int id)
        {
            return _context.CoursesInfo.Any(e => e.CourseInfoId == id);
        }
    }
}
