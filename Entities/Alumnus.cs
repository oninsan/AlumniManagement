using System;
using System.Collections.Generic;

namespace AlumniManagement.Entities;


public enum ConfirmationStatus
{
    Pending = 0,   // 0
    Declined = 1,  // 1
    Confirmed = 2  // 2
}

public partial class Alumnus
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string CourseGraduated { get; set; } = null!;

    public string Department { get; set; } = null!;

    public short YearGraduated { get; set; }

    public bool WorkingStatus { get; set; }

    public string CurrentWork { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public ConfirmationStatus ConfirmationStatus { get; set; } = ConfirmationStatus.Pending;
}
