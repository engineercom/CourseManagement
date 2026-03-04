namespace CourseManagement.Api.Dtos.CourseDtos
{
    public class StudentAssignCoursesDto
    {
        public int StudentId { get; set; }
        public List<int> CourseIds { get; set; }=new List<int>();
    }
}
