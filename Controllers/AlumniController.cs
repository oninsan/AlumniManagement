using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumniManagement.Entities;
using System.Security.Policy;
using System.Net.Mail;
using System.Net.Http;
using System.Net;

namespace AlumniManagement.Controllers
{
    public class AlumniController : Controller
    {
        private readonly AlumniManagementContext _context;

        public AlumniController(AlumniManagementContext context)
        {
            _context = context;
        }

        // GET: Alumni
        public async Task<IActionResult> Index(string course, int? yearGraduated, string department)
        {
            var alumniQuery = _context.Alumni.AsQueryable();

            if (!string.IsNullOrEmpty(course))
            {
                alumniQuery = alumniQuery.Where(a => a.CourseGraduated == course);
            }

            if (yearGraduated.HasValue)
            {
                alumniQuery = alumniQuery.Where(a => a.YearGraduated == yearGraduated.Value);
            }

            if (!string.IsNullOrEmpty(department))
            {
                alumniQuery = alumniQuery.Where(a => a.Department == department);
            }

            return _context.Alumni != null ? 
                          View(await alumniQuery.ToListAsync()) :
                          Problem("Entity set 'AlumniManagementContext.Alumni'  is null.");
        }

        // GET: Alumni/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumni == null)
            {
                return NotFound();
            }

            var alumnus = await _context.Alumni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumnus == null)
            {
                return NotFound();
            }

            return View(alumnus);
        }

        // GET: Alumni/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Username,Role,Password,CourseGraduated,Department,YearGraduated,WorkingStatus,CurrentWork, MobileNumber, Email, Picture")] Alumnus alumnus)
        {
            if (ModelState.IsValid)
            {
                alumnus.ConfirmationStatus = 0;

                _context.Add(alumnus);
                await _context.SaveChangesAsync();
                //Email sending
                var emailContent = "Hello " + alumnus.FirstName + "<br>Greetings from Alumni Management" +
                                  "<br>Your username is: " + alumnus.Username + "<br>Your password is: " + alumnus.Password 
                                  + "<br>Click this to login <a href='https://alumnihub.bsite.net'>Alumni Management</a>";


                var mailMessage = new MailMessage

                {

                    From = new MailAddress("admin@alumnihub.online"), // Replace with your email

                    Subject = "Alumni Management System greetings",

                    Body = emailContent,

                    IsBodyHtml = true,

                };

                mailMessage.To.Add(alumnus.Email); // Add the recipient's email


                using (var smtpClient = new SmtpClient())

                {

                    smtpClient.Host = "smtp.hostinger.com"; // Replace with your SMTP host

                    smtpClient.Port = 587; // Replace with your SMTP port

                    smtpClient.EnableSsl = true; // Replace with your SSL setting

                    smtpClient.Credentials = new NetworkCredential("admin@alumnihub.online", "Alumni_2024"); // Replace with your email and password


                    smtpClient.Send(mailMessage);


                }
                 //End of Email sending
               
                return RedirectToAction(nameof(Index));
            }
            return View(alumnus);
        }

        // GET: Alumni/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumni == null)
            {
                return NotFound();
            }

            var alumnus = await _context.Alumni.FindAsync(id);
            if (alumnus == null)
            {
                return NotFound();
            }
            return View(alumnus);
        }

        // POST: Alumni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Username,Password, Role,CourseGraduated,YearGraduated,WorkingStatus,CurrentWork, MobileNumber, Email, Department, Picture, ConfirmationStatus")] Alumnus alumnus)
        {
            if (id != alumnus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var originalAlumnus = await _context.Alumni.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);


                    if (originalAlumnus == null)
                    {
                        return NotFound();
                    }


                    _context.Update(alumnus);
                    await _context.SaveChangesAsync();

                    if (originalAlumnus.ConfirmationStatus != alumnus.ConfirmationStatus)
                    {
                        string emailSubject = "";
                        string emailBody = "";

                        switch (alumnus.ConfirmationStatus)
                        {
                            case ConfirmationStatus.Confirmed:
                                emailSubject = "Your account has been approved!";
                                emailBody = $"Hello {alumnus.FirstName},<br><br>" +
                                            "Good news! Your account has been approved by the admin.<br>" +
                                            "You can now log in to the Alumni Management system using your credentials. <br>" +
                                            "Click here to login: <a href='https://alumnihub.bsite.net'>Alumni Management</a><br><br>" +
                                            "Thank you for being part of our community.";
                                break;

                            case ConfirmationStatus.Declined:
                                emailSubject = "Your account has been declined";
                                emailBody = $"Hello {alumnus.FirstName},<br><br>" +
                                            "We're sorry to inform you that your account has been declined by the admin.<br>" +
                                            "If you believe this is an error, please contact the support team.<br>" +
                                            "Thank you for your understanding.";
                                break;

                            case ConfirmationStatus.Pending:
                                emailSubject = "Your account is on hold";
                                emailBody = $"Hello {alumnus.FirstName},<br><br>" +
                                            "Your account is currently on hold and awaiting admin approval.<br>" +
                                            "Once the admin reviews and approves your account, you'll be notified.<br><br>" +
                                            "Thank you for your patience.";
                                break;

                            default:
                                return RedirectToAction(nameof(Index)); 
                        }

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("admin@alumnihub.online"),
                            Subject = emailSubject,
                            Body = emailBody,
                            IsBodyHtml = true,
                        };

                        mailMessage.To.Add(alumnus.Email);

                        using (var smtpClient = new SmtpClient())
                        {
                            smtpClient.Host = "smtp.hostinger.com"; // Use the appropriate SMTP host for your server
                            smtpClient.Port = 587;
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new NetworkCredential("admin@alumnihub.online", "Alumni_2024"); // Replace with your SMTP credentials

                            await smtpClient.SendMailAsync(mailMessage);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnusExists(alumnus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alumnus);
        }

        // GET: Alumni/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumni == null)
            {
                return NotFound();
            }

            var alumnus = await _context.Alumni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumnus == null)
            {
                return NotFound();
            }

            return View(alumnus);
        }

        // POST: Alumni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alumni == null)
            {
                return Problem("Entity set 'AlumniManagementContext.Alumni'  is null.");
            }
            var alumnus = await _context.Alumni.FindAsync(id);
            if (alumnus != null)
            {
                _context.Alumni.Remove(alumnus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnusExists(int id)
        {
          return (_context.Alumni?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
