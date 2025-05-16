using Fitness.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Fitness.Areas.Admin.Controllers;
[Area("Admin")]
public class CourseController (AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.ToListAsync();
        return View(courses);
    }
}
