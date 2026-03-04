using CourseManagement.WebUI.Dtos.InstructorDtos;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.WebUI.Controllers;

public class InstructorController : Controller
{
    private readonly IHttpClientFactory _factory;
    public InstructorController(IHttpClientFactory factory)=>_factory=factory;

    public async Task< IActionResult> Index()
    {
        var client=_factory.CreateClient("api");
        var response=await client.GetFromJsonAsync<List<InstructorDto>>("api/instructors");
        return View(response);
    }
}
