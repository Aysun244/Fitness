using Microsoft.AspNetCore.Mvc;

namespace Fitness.Areas.Admin.Controllers;
[Area("Admin")]
public class TeacherController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
