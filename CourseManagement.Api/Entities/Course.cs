using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Api.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // [Precision(18,2)]
        public decimal Price { get; set; }

        public List<StudentCourse> Students { get; set; } = new List<StudentCourse>();
        public List<CourseInstructor> Instructors { get; set; } = new List<CourseInstructor>();
    }
}
