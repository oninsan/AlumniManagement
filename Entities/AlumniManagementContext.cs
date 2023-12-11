using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=alumni_management;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumnus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alumni");

            entity.Property(e => e.Id)
                .HasColumnType("int(20)")
                .HasColumnName("id");
            entity.Property(e => e.CourseGraduated)
                .HasMaxLength(50)
                .HasColumnName("course_graduated");
            entity.Property(e => e.CurrentWork)
                .HasMaxLength(50)
                .HasColumnName("current_work");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.WorkingStatus).HasColumnName("working_status");
            entity.Property(e => e.YearGraduated)
                .HasColumnType("year(4)")
                .HasColumnName("year_graduated");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("announcement");

            entity.Property(e => e.Id)
                .HasColumnType("int(20)")
                .HasColumnName("id");
            entity.Property(e => e.AnnouncementDescription)
                .HasMaxLength(2000)
                .HasColumnName("announcement_description");
            entity.Property(e => e.AnnouncementTitle)
                .HasMaxLength(50)
                .HasColumnName("announcement_title");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("event");

            entity.Property(e => e.Id)
                .HasColumnType("int(20)")
                .HasColumnName("id");
            entity.Property(e => e.AttendeesList)
                .HasMaxLength(2000)
                .HasColumnName("attendees_list");
            entity.Property(e => e.EventDate).HasColumnName("event_date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(2000)
                .HasColumnName("event_description");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .HasColumnName("event_name");
            entity.Property(e => e.EventTime)
                .HasColumnType("time")
                .HasColumnName("event_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
