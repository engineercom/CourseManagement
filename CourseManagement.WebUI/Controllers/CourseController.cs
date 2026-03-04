
using CourseManagement.WebUI.Dtos.CourseDtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagement.WebUI.Controllers;

public class CourseController : Controller
{
    private IHttpClientFactory _factory;

    public CourseController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<IActionResult> Index()
    {
        var client=_factory.CreateClient("api");
        var response =await client.GetFromJsonAsync<List<CourseDto>>("api/courses");
        return View(response);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto dto)
    {
        var client = _factory.CreateClient("api");
        var response = await client.PostAsJsonAsync("api/courses",dto);
        response.EnsureSuccessStatusCode();
        return RedirectToAction("Index");
    }
}
