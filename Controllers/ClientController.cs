using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumniManagement.Entities;

namespace AlumniManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ClientController: ControllerBase
    {
        private readonly AlumniManagementContext _context;
        public ClientController(AlumniManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetAnnouncements()
        {
            return new OkObjectResult( await _context.Announcements.Take(5).OrderByDescending(a=>a.Id).ToListAsync());
        }

        public async Task<IActionResult> GetEvents()
        {
            return new OkObjectResult( await _context.Events.Take(5).OrderByDescending(a=>a.Id).ToListAsync() );
        }

        public async Task<IActionResult> GetTotalEntityItems()
        {
            var total_items = new Dictionary<string, int> {
                { "TotalAlumni", await _context.Alumni.CountAsync() },
                {"TotalAnnouncements", await _context.Announcements.CountAsync() },
                { "TotalEvents", await _context.Events.CountAsync() }
            };
            return new OkObjectResult(total_items);
        }

    }
}