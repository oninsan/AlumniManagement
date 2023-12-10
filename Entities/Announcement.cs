using System;
using System.Collections.Generic;

namespace AlumniManagement.Entities;

public partial class Announcement
{
    public int Id { get; set; }

    public string AnnouncementTitle { get; set; } = null!;

    public string AnnouncementDescription { get; set; } = null!;
}
