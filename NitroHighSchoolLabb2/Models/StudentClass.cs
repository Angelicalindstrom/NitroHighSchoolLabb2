using System.ComponentModel.DataAnnotations;

namespace NitroHighSchoolLabb2.Models
{
    public class StudentClass
    {
        public int StudentClassId { get; set; }
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string? StudentClassName { get; set; }

        public virtual ICollection<Student>? Students { get; set; }
        // kopplas till CourseClasses i Db
    }
}
