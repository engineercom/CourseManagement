namespace CourseManagement.Api.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Expertise { get; set; }

        public List<CourseInstructor> Courses { get; set; }=new List<CourseInstructor>();
    }
}
