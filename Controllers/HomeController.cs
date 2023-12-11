using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AlumniManagement.Models;
using AlumniManagement.Entities;


namespace AlumniManagement.Controllers;

public class HomeController : Controller
{
    // private readonly ILogger<HomeController> _logger;
    private readonly AlumniManagementContext _context;
    public HomeController(AlumniManagementContext context)
    {
        // _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Alumni.OrderByDescending(a => a.Id).Take(10).ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
