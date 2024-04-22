using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace NitroHighSchoolLabb2.Models
{
    public enum Status
    {
       NoInfo, Good, Great, Bad
    }
    public class CourseInfo
    {
        public int CourseInfoId { get; set; }

        public Status? Status { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set;}
        public virtual Course? Course { get; set; }
        // Hämtar från

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        // Hämtar från

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
        // Hämtar från


        // en Courseklass kan ha olika ämnen(course)
        // många studenter kan gå olika kursklasser
        // många lärare kan undervis amånga kursklasser
    }
}
