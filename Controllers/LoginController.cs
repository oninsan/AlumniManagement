using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AlumniManagement.Models;
using AlumniManagement.Entities;

namespace AlumniManagement.Controllers;
public class LoginController: Controller
{
    private readonly AlumniManagementContext _context;
    public LoginController(AlumniManagementContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        return View();
    }
}