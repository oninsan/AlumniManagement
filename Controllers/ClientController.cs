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
using System.Security.Cryptography.Pkcs;
using System.Runtime.CompilerServices;

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
                if(account.Role == "admin")
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
                            { "currentWork", account.CurrentWork },
                            { "mobileNumber", account.MobileNumber },
                            { "email", account.Email }
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
                        { "currentWork", account.CurrentWork },
                        { "mobileNumber", account.MobileNumber },
                        { "email", account.Email }
                    });
            }
            return new OkObjectResult(
                new Dictionary<string, string>{
                    { "role", "none" },
                    { "logged", "false" }
                });
        }

        public IActionResult GetEmploymentRate()
        {
            return new JsonResult(
                new Dictionary<string, object>{
                    {"ccs", new Dictionary<string, object>{
                        { "population", _context.Alumni.Where(c=>c.CourseGraduated == "BSIT").Count() },
                        { "employed", _context.Alumni.Where(c=>c.CourseGraduated == "BSIT" && c.WorkingStatus == true).Count() }
                    }},
                    {"ccje", new Dictionary<string,object>{
                        { "population",_context.Alumni.Where(c=>c.CourseGraduated == "BSCRIM").Count()},
                        { "employed", _context.Alumni.Where(c=>c.CourseGraduated == "BSCRIM" && c.WorkingStatus == true).Count() }
                    }},
                    {"cte", new Dictionary<string, object>{
                        { "population",_context.Alumni.Where(c=>c.CourseGraduated == "BSED").Count() },
                        { "employed", _context.Alumni.Where(c=>c.CourseGraduated == "BSED" && c.WorkingStatus == true).Count() }
                    }},
                    {"psych", new Dictionary<string, object>{
                        { "population",_context.Alumni.Where(c=>c.CourseGraduated == "PSYCHOLOGY").Count() },
                        { "employed", _context.Alumni.Where(c=>c.CourseGraduated == "PSYCHOLOGY" && c.WorkingStatus == true).Count() }
                    }},
                    {"coc", new Dictionary<string, object>{
                        { "population",_context.Alumni.Where(c=>c.CourseGraduated == "COMMERCE").Count() },
                        { "employed", _context.Alumni.Where(c=>c.CourseGraduated == "COMMERCE" && c.WorkingStatus == true).Count() }
                    }},
                }
            );
        }

        // Edit the alumni profile
        [HttpPost]
        public async Task<IActionResult> EditProfile(int id, string username, string firstname, string lastname, string coursename, string yeargraduated, string password, bool workingstatus, string currentwork)
        {
            var alumnus = _context.Alumni.FirstOrDefault(a=>a.Id==id);
            if(alumnus != null)
            {
                alumnus.Username = username;
                alumnus.FirstName = firstname;
                alumnus.LastName = lastname;
                alumnus.CourseGraduated = coursename;
                alumnus.YearGraduated = short.Parse(yeargraduated);
                alumnus.Password = password;
                alumnus.WorkingStatus = workingstatus;
                alumnus.CurrentWork = currentwork;
                await _context.SaveChangesAsync();
                return new JsonResult(alumnus);
            }
            return new BadRequestObjectResult("Alumnus not found!");
        }

        // add comments in events
        [HttpPost]
        public async Task<IActionResult> AddEventComment(EventComment eventComment)
        {
            if(eventComment != null)
            {
                await _context.EventComments.AddAsync(eventComment);
                await _context.SaveChangesAsync();
                return new JsonResult("Successfully added a comment");
            }
            return new BadRequestObjectResult("No comment added");
        }

        // get all event comments
        public IActionResult CountEventComments(int eventid)
        {
            return new JsonResult(_context.EventComments.Where(e=>e.EventId==eventid).Count());
        }

        // add event like
        [HttpPost]
        public async Task<IActionResult> AddEventLike(EventLike eventLike)
        {
            if(eventLike != null)
            {
                var like = await _context.EventLikes.FirstOrDefaultAsync(l=>l.UserId == eventLike.UserId && l.EventId == eventLike.EventId);
                if(like == null)
                {
                    await _context.EventLikes.AddAsync(eventLike);
                }
                else
                {
                    like.Status = !like.Status;
                }
                await _context.SaveChangesAsync();
                return new JsonResult("success");
            }
            return new BadRequestObjectResult("unsuccessful like");
        }

        // get like status for each event
        public async Task<IActionResult> GetLikeStatus(int userid, int eventid)
        {
            var like = await _context.EventLikes.FirstOrDefaultAsync(l=>l.UserId==userid && l.EventId==eventid);
            if(like != null)
            {
                return new JsonResult(like.Status);
            }
            return new JsonResult(false);
        }

        // get all event comments
        public IActionResult CountEventLikes(int eventid)
        {
            return new JsonResult(_context.EventLikes.Where(e=>e.EventId==eventid && e.Status==true).Count());
        }
    }
}