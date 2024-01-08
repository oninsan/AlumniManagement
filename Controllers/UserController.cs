using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumniManagement.Entities;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace AlumniManagement.Controllers
{
    public class UserController:Controller
    {
        private readonly AlumniManagementContext _context;
        public UserController(AlumniManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Events()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}