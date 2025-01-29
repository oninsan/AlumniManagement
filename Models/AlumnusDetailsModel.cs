using AlumniManagement.Entities;
namespace AlumniManagement.Models;
public class AlumnusDetailsViewModel
{
    public Alumnus? Alumnus { get; set; }
    public List<EmploymentHistory> EmploymentHistory { get; set; } = new List<EmploymentHistory>();
}