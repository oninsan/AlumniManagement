using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AlumniManagement.Models;
using AlumniManagement.Entities;
using OfficeOpenXml;


namespace AlumniManagement.Controllers;

public class HomeController : Controller
{
    private readonly AlumniManagementContext _context;
    public HomeController(AlumniManagementContext context)
    {
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

    public IActionResult Login()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult GenerateEmploymentReport()
    {
        var alumniData = _context.Alumni.ToList();
        var eventData = _context.Events.ToList();

        using (ExcelPackage package = new ExcelPackage())
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Employment Report");

            worksheet.Cells[1, 1].Value = "First Name";
            worksheet.Cells[1, 2].Value = "Last Name";
            worksheet.Cells[1, 3].Value = "Department";
            worksheet.Cells[1, 4].Value = "Course Graduated";
            worksheet.Cells[1, 5].Value = "Employment Status";
            worksheet.Cells[1, 6].Value = "Current Work";

            int row = 2;
            int employedCount = 0;
            int unemployedCount = 0;

            foreach (var alumnus in alumniData)
            {
                worksheet.Cells[row, 1].Value = alumnus.FirstName;
                worksheet.Cells[row, 2].Value = alumnus.LastName;
                worksheet.Cells[row, 3].Value = alumnus.Department;
                worksheet.Cells[row, 4].Value = alumnus.CourseGraduated;
                if (alumnus.WorkingStatus)
                {
                    worksheet.Cells[row, 5].Value = "Employed";
                    worksheet.Cells[row, 6].Value = alumnus.CurrentWork;
                    employedCount++;  
                }
                else
                {
                    worksheet.Cells[row, 5].Value = "Unemployed";
                    worksheet.Cells[row, 6].Value = "N/A";
                    unemployedCount++; 
                }

                row++;
            }

            worksheet.Cells[row, 5].Value = "Employed Count:";
            worksheet.Cells[row, 6].Value = employedCount;

            row++;
            worksheet.Cells[row, 5].Value = "Unemployed Count:";
            worksheet.Cells[row, 6].Value = unemployedCount;


            ExcelWorksheet eventParticipationWorksheet = package.Workbook.Worksheets.Add("Event Participation");

            eventParticipationWorksheet.Cells[1, 1].Value = "Event Name";
            eventParticipationWorksheet.Cells[1, 2].Value = "Event Description";
            eventParticipationWorksheet.Cells[1, 3].Value = "Event Date";
            eventParticipationWorksheet.Cells[1, 4].Value = "Event Time";
            eventParticipationWorksheet.Cells[1, 5].Value = "Alumnus Name";

            int eventParticipationRow = 2;

            foreach (var eventItem in eventData)
            {
                eventParticipationWorksheet.Cells[eventParticipationRow, 1].Value = eventItem.EventName;
                eventParticipationWorksheet.Cells[eventParticipationRow, 2].Value = eventItem.EventDescription;
                eventParticipationWorksheet.Cells[eventParticipationRow, 3].Value = eventItem.EventDate.ToString("yyyy-MM-dd");
                eventParticipationWorksheet.Cells[eventParticipationRow, 4].Value = eventItem.EventTime.ToString("HH:mm");
                eventParticipationWorksheet.Cells[eventParticipationRow, 5].Value = eventItem.AttendeesList;

                eventParticipationRow++;
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;
            string excelName = $"EmploymentReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
