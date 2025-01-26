using System;
using System.Collections.Generic;

namespace AlumniManagement.Entities;

public partial class EventComment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int EventId { get; set; }

    public string Comment { get; set; } = null!;
}
