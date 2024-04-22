using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NitroHighSchoolLabb2.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string? StudentName { get; set;}

        // fk
        [ForeignKey("StudentClass")]
        public int FkStudentClassId { get; set; }
        public virtual StudentClass? StudentClass { get; set; }
        // Hämtar från

        public virtual ICollection<CourseInfo>? CourseClasses { get; set; }
        // kopplas till CourseClasses i Db
    }
}
