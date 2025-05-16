using System.Threading.Tasks;
using Fitness.DataAccessLayer;
using Fitness.Models;
using Fitness.ViewModels.CourseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Fitness.Areas.Admin.Controllers;
[Area("Admin")]
public class CourseController (AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.Select(x=>new CourseGetVM()
        {
            Id=x.Id,
            Name = x.Name,
            Description = x.Description,
            ImagePath = x.ImagePath,
            Time = x.Time,
        }).ToListAsync();
        return View(courses);
    }

    public async Task<IActionResult> Create()
    {         
                return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult>Create(CourseCreateVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        if(vm.ImageFile.Length>2*1024*1024)
        {
            ModelState.AddModelError("Image", "Elave edilen fayl 2mb'dan boyuk olmamalidir");
            return View(vm);
        }
        if (!vm.ImageFile.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "Yalniz sekil fotmatinda file elave edin");
            return View(vm);
        }

        var isExistCategory = await _context.Courses.AnyAsync(x=>x.Id==vm.TeacherId);
        if (isExistCategory is false)
        {
            ModelState.AddModelError("TeacherId", "Bu id'li muellim heleki movcud deyil");
            return View(vm);
        }


        string filename = Guid.NewGuid().ToString() + vm.ImageFile.FileName;
        string path = Path.Combine( "wwwroot","images",filename);

        using FileStream stream = new(path, FileMode.OpenOrCreate);
       await vm.ImageFile.CopyToAsync(stream);

        Course course = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            ImagePath = filename,
            Time = vm.Time,
            TeacherId = vm.TeacherId,
        };
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
