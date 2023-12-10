using System;
using System.Collections.Generic;

namespace AlumniManagement.Entities;

public partial class Event
{
    public int Id { get; set; }

    public string EventName { get; set; } = null!;

    public string EventDescription { get; set; } = null!;

    public DateOnly EventDate { get; set; }

    public DateOnly EventTime { get; set; }

    public string AttendeesList { get; set; } = null!;
}
