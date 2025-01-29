namespace AlumniManagement.Entities;
public class EmploymentHistory
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string JobDescription { get; set; } = null!;

    public int UserId { get; set; }
}