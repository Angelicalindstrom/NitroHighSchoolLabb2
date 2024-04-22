namespace NitroHighSchoolLabb2.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public virtual ICollection<CourseInfo>? CoursesClasses { get; set; }
        // kopplas till CourseClasses i Db
    }
}
