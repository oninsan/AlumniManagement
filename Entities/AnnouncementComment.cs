using System;
using System.Collections.Generic;

namespace AlumniManagement.Entities;

public partial class AnnouncementComment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AnnouncementId { get; set; }

    public string Comment { get; set; } = null!;
}
