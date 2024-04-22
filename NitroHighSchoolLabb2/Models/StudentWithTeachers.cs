namespace NitroHighSchoolLabb2.Models
{
    public class StudentWithTeachers
    {
        public string CourseName { get; set; }
        public IEnumerable<string> TeacherNames { get; set; }
        public IEnumerable<string> StudentNames { get; set; }
    }
}
