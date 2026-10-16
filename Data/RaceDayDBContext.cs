using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;
using RaceDayRoute = RaceDay.API.Models.Route;

namespace RaceDayAPI.Data
{
    public class RaceDayDBContext : DbContext
    {
        public RaceDayDBContext(
        DbContextOptions<RaceDayDBContext> options)
        : base(options)
        {
        }


    public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<RaceDayRoute> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Match the existing SQL Server table names.
            modelBuilder.Entity<UserAccount>().ToTable("UserAccount");
            modelBuilder.Entity<Organiser>().ToTable("Organiser");
            modelBuilder.Entity<Participant>().ToTable("Participant");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<EventCategory>().ToTable("EventCategory");
            modelBuilder.Entity<Enrollment>().ToTable("Enrolment");
            modelBuilder.Entity<Result>().ToTable("Result");
            modelBuilder.Entity<RaceDayRoute>().ToTable("Route");

            // Map the C# enrolment properties to the SQL column names.
            modelBuilder.Entity<Enrollment>()
                .Property(e => e.EnrollmentID)
                .HasColumnName("EnrolmentID");

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.EnrollmentDate)
                .HasColumnName("EnrolmentDate");

            // Map the result foreign key to the SQL column name.
            modelBuilder.Entity<Result>()
                .Property(r => r.EnrollmentID)
                .HasColumnName("EnrolmentID");

            // A user account can have one organiser profile.
            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.Organiser)
                .WithOne(o => o.UserAccount)
                .HasForeignKey<Organiser>(o => o.UserAccountID);

            // A user account can have one participant profile.
            modelBuilder.Entity<UserAccount>()
                .HasOne(u => u.Participant)
                .WithOne(p => p.UserAccount)
                .HasForeignKey<Participant>(p => p.UserAccountID);

            // An event references one route.
            // A route can be used by multiple events.
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Route)
                .WithMany()
                .HasForeignKey(e => e.RouteID);

            // An event category belongs to one event.
            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventCategories)
                .HasForeignKey(ec => ec.EventID);

            // An event category references one category.
            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Category)
                .WithMany()
                .HasForeignKey(ec => ec.CategoryID);

            // An enrolment belongs to one participant.
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Participant)
                .WithMany()
                .HasForeignKey(e => e.ParticipantID);

            // An enrolment belongs to one event category.
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.EventCategory)
                .WithMany(ec => ec.Enrollments)
                .HasForeignKey(e => e.EventCategoryID);

            // An enrolment can have one result.
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Result)
                .WithOne(r => r.Enrollment)
                .HasForeignKey<Result>(r => r.EnrollmentID);

            // Category names must be unique.
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            // Each participant can enrol in a particular event category once.
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.ParticipantID, e.EventCategoryID })
                .IsUnique();

            // Each event/category combination must be unique.
            modelBuilder.Entity<EventCategory>()
                .HasIndex(ec => new { ec.EventID, ec.CategoryID })
                .IsUnique();

            // Each result can belong to only one enrolment.
            modelBuilder.Entity<Result>()
                .HasIndex(r => r.EnrollmentID)
                .IsUnique();
        }
    }


}

