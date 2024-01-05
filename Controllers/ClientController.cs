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
            return new OkObjectResult( await _context.Announcements.OrderByDescending(a=>a.Id).Take(5).ToListAsync());
        }

        public async Task<IActionResult> GetAlumniAnnouncements()
        {
            return new OkObjectResult( await _context.Announcements.OrderByDescending(a=>a.Id).ToListAsync() );
        }

        public async Task<IActionResult> GetEvents()
        {
            return new OkObjectResult( await _context.Events.OrderByDescending(a=>a.Id).Take(5).ToListAsync() );
        }

        public async Task<IActionResult> GetAllEvents()
        {
            return new OkObjectResult( await _context.Events.OrderByDescending(i=>i.Id).ToListAsync() );
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
        public async Task<IActionResult> UserLogin(string username, string password)
        {
            var account = await _context.Alumni
                .FirstOrDefaultAsync(a=>a.Username == username && 
                                        a.Password == password);
            if(account !=  null)
            {
                if(account.Username == "oninsan" && account.Password == "12345")
                {
                    return new OkObjectResult(
                        new Dictionary<string, object>{
                            { "ID", account.Id },
                            { "role", "admin" },
                            { "logged", "true" },
                            { "firstName", account.FirstName },
                            { "lastName", account.LastName },
                            { "userName", account.Username },
                            { "password", account.Password },
                            { "courseGraduated", account.CourseGraduated },
                            { "yearGraduated", account.YearGraduated },
                            { "workingStatus", account.WorkingStatus },
                            { "currentWork", account.CurrentWork }
                        });
                }
                return new OkObjectResult(
                    new Dictionary<string, object>{
                        { "ID", account.Id },
                        { "role", "user" },
                        { "logged", "true" },
                        { "firstName", account.FirstName },
                        { "lastName", account.LastName },
                        { "userName", account.Username },
                        { "password", account.Password },
                        { "courseGraduated", account.CourseGraduated },
                        { "yearGraduated", account.YearGraduated },
                        { "workingStatus", account.WorkingStatus },
                        { "currentWork", account.CurrentWork }
                    });
            }
            return new OkObjectResult(
                new Dictionary<string, string>{
                    { "role", "none" },
                    { "logged", "false" }
                });
        }
    }
}