using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlumniManagement.Entities;

public partial class AlumniManagementContext : DbContext
{
    public AlumniManagementContext()
    {
    }

    public AlumniManagementContext(DbContextOptions<AlumniManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumnus> Alumni { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AnnouncementComment> AnnouncementComments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventComment> EventComments { get; set; }

    public virtual DbSet<EventLike> EventLikes { get; set; }

    public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     //optionsBuilder.UseSqlServer("Server=WAR-MACHINE\\SQLEXPRESS;Database=alumni_management;Integrated Security=True;TrustServerCertificate=True;");
    //     optionsBuilder.UseSqlServer("Server=sql.bsite.net\\MSSQL2016;Database=alumnihub_;User Id=alumnihub_;Password=admin;TrustServerCertificate=True;");
    // }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PAAQDTO\\SQLEXPRESS;Database=alumni_management;TrustServerCertificate=true;Trusted_Connection=True;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumnus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Alumni");
            entity.ToTable("alumni");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.CourseGraduated)
                .HasMaxLength(100)
                .HasColumnName("course_graduated");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
            entity.Property(e => e.CurrentWork)
                .HasMaxLength(100)
                .HasColumnName("current_work");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Picture)
                .HasMaxLength(100)
                .HasColumnName("picture");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(100)
                .HasColumnName("mobile_number");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username")
                .HasDefaultValueSql("user");
            entity.Property(e => e.WorkingStatus).HasColumnName("working_status");
            entity.Property(e => e.YearGraduated)
                .HasColumnType("int")
                .HasColumnName("year_graduated");
            entity.Property(e => e.ConfirmationStatus)
                .HasConversion<int>()
                .HasColumnName("confirmation_status")
                .HasDefaultValueSql("0");
        });

        modelBuilder.Entity<EmploymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EmploymentHistory");
            entity.ToTable("employment_history");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .HasColumnName("company_name");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .HasColumnName("position");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.JobDescription)
                .HasMaxLength(2000)
                .HasColumnName("job_description");
            entity.Property(e => e.UserId)
                 .HasColumnType("int")
                 .HasColumnName("user_id");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.HasKey(e => e.Id).HasName("PK_Announcement");
            entity.ToTable("announcement");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.AnnouncementDescription)
                .HasMaxLength(2000)
                .HasColumnName("announcement_description");
            entity.Property(e => e.AnnouncementTitle)
                .HasMaxLength(100)
                .HasColumnName("announcement_title");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("date_created");
        });

        modelBuilder.Entity<AnnouncementComment>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.HasKey(e => e.Id).HasName("PK_AnnouncementComment");

            entity.ToTable("announcement_comment");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.AnnouncementId)
                .HasColumnType("int")
                .HasColumnName("announcement_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(2000)
                .HasColumnName("comment");
            entity.Property(e => e.UserId)
                .HasColumnType("int")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.HasKey(e => e.Id).HasName("PK_Event");

            entity.ToTable("event");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.AttendeesList)
                .HasMaxLength(2000)
                .HasColumnName("attendees_list");
            entity.Property(e => e.EventDate)
                .HasColumnType("date")  // Use 'date' in MySQL
                .HasConversion(
                    v => v.ToDateTime(new TimeOnly(0, 0)),  // Convert DateOnly to DateTime
                    v => DateOnly.FromDateTime(v));
            entity.Property(e => e.EventDescription)
                .HasMaxLength(2000)
                .HasColumnName("event_description");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .HasColumnName("event_name");
            // Apply the value converter to convert TimeOnly to TimeSpan (and back)
            var timeConverter = new ValueConverter<TimeOnly, TimeSpan>(
                v => v.ToTimeSpan(),  // TimeOnly to TimeSpan
                v => TimeOnly.FromTimeSpan(v)); // TimeSpan to TimeOnly

            entity.Property(e => e.EventTime)
                .HasColumnType("time")
                .HasColumnName("event_time")
                .HasConversion(timeConverter);
        });

        modelBuilder.Entity<EventComment>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.HasKey(e => e.Id).HasName("PK_EventComment");

            entity.ToTable("event_comment");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasMaxLength(2000)
                .HasColumnName("comment");
            entity.Property(e => e.EventId)
                .HasColumnType("int")
                .HasColumnName("event_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int")
                .HasColumnName("user_id");
        });

        modelBuilder.Entity<EventLike>(entity =>
        {
            //entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.HasKey(e => e.Id).HasName("PK_EventLike");

            entity.ToTable("event_like");

            entity.Property(e => e.Id)
                .HasColumnType("int")
                .HasColumnName("id");
            entity.Property(e => e.EventId)
                .HasColumnType("int")
                .HasColumnName("event_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasColumnType("int")
                .HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
