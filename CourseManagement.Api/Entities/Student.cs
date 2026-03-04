namespace CourseManagement.Api.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public List<StudentCourse> Courses { get; set; }= new List<StudentCourse>();
    }
}
