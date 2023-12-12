using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumniManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AlumniManagement.Controllers
{
    [Route("api/[controller]/[action]")]
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

        public async Task<IActionResult> GetAlumniAnnouncements()
        {
            return new OkObjectResult( await _context.Announcements.OrderByDescending(a=>a.Id).ToListAsync() );
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

        [HttpPost]
        public async Task<IActionResult> UserLogin(String username, String password)
        {
            var account = await _context.Alumni
                .FirstOrDefaultAsync(a=>a.Username == username && 
                                        a.Password == password);
            if(account ==  null)
            {
                return new BadRequestObjectResult("Wrong username or password!");
            }
            return new OkObjectResult("Successfully logged in");
        }
    }
}