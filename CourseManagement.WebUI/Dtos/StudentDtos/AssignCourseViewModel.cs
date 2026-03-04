namespace CourseManagement.WebUI.Dtos.StudentDtos;

public class AssignCourseViewModel
{
    public int Id { get; set; }
    public List<CourseCheckboxItem> Courses { get; set; }

}
public class CourseCheckboxItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
}


