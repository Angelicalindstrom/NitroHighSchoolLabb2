namespace NitroHighSchoolLabb2.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }

        public virtual ICollection<CourseInfo>? CourseClasses { get; set; }
        // kopplas till CourseClasses i Db
    }
}
