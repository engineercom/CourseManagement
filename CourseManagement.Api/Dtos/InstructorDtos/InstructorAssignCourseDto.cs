namespace CourseManagement.Api.Dtos.InstructorDtos
{
    public class InstructorAssignCourseDto
    {
        public int InstructorId { get; set; }
        public List<int> Courses { get; set; }
    }
}
