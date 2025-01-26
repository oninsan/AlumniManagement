using System;
using System.Collections.Generic;

namespace AlumniManagement.Entities;

public partial class EventLike
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int EventId { get; set; }

    public bool Status { get; set; }
}
