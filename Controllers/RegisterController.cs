using Microsoft.AspNetCore.Mvc;
using AlumniManagement.Entities;
using System.Net.Mail;
using System.Net;
using System.Data;

namespace AlumniManagement.Controllers
{
    public class RegisterController : Controller
    {
        private readonly AlumniManagementContext _context;

        public RegisterController(AlumniManagementContext context)
        {
            _context = context;
        }

        // GET: Register/Index
        public IActionResult Index()
        {
            // Fetch data for courses, years, and departments
            var courses = _context.Alumni.Select(a => a.CourseGraduated).Distinct().ToList();
            var years = _context.Alumni.Select(a => a.YearGraduated).Distinct().ToList();
            var departments = _context.Alumni.Select(a => a.Department).Distinct().ToList();

            // Pass data to the view
            ViewBag.Courses = courses;
            ViewBag.Years = years;
            ViewBag.Departments = departments;

            // Return an empty model for the form
            var alumnus = new Alumnus();
            return View(alumnus);
        }

        // POST: Register/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,FirstName,LastName,Username,Role,Password,CourseGraduated,Department,YearGraduated,WorkingStatus,CurrentWork,MobileNumber,Email,Picture")] Alumnus alumnus)
        {
  
            if (ModelState.IsValid)
            {

                alumnus.ConfirmationStatus = 0;
                // Add the alumnus to the database
                _context.Add(alumnus);
                await _context.SaveChangesAsync();

                // Prepare the email content
                var emailContent = $"Hello {alumnus.FirstName},<br><br>" +
                                   "Greetings from Alumni Management.<br>" +
                                   $"Your username is: {alumnus.Username}<br>" +
                                   "Click this to login: <a href='https://alumnihub.bsite.net'>Alumni Management</a><br><br>" +
                                   "Your registration is successful. However, it is awaiting admin approval.";

                // Send the email
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("admin@alumnihub.online"),
                    Subject = "Alumni Management System: Registration Success",
                    Body = emailContent,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(alumnus.Email);

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.hostinger.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("admin@alumnihub.online", "Alumni_2024"); // Replace with your SMTP credentials

                    await smtpClient.SendMailAsync(mailMessage);
                }

                TempData["SuccessMessage"] = "Registration successful! Please wait for admin approval.";

                return RedirectToAction("Index", "Register");
            }

            return View(alumnus);
        }
    }
}
