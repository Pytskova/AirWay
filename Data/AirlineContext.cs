// Data/AirlineContext.cs
using Microsoft.EntityFrameworkCore;
using AirlineApp.Models;

namespace AirlineApp.Data
{
    public class AirlineContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightLog> FlightLogs { get; set; }
        public DbSet<CrewAssignment> CrewAssignments { get; set; }
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public DbSet<TechnicalReport> TechnicalReports { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<TrainingEvent> TrainingEvents { get; set; }
        public DbSet<EmployeeTraining> EmployeeTrainings { get; set; }

        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<PilotReport> PilotReports { get; set; }
        public DbSet<PartRequest> PartRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=AirlineDb;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CrewAssignment>()
                .HasKey(c => new { c.FlightId, c.EmployeeId });

            modelBuilder.Entity<EmployeeTraining>()
                .HasKey(et => new { et.EventId, et.EmployeeId });

            modelBuilder.Entity<CrewAssignment>()
                .HasOne(c => c.Flight)
                .WithMany(f => f.CrewAssignments)
                .HasForeignKey(c => c.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CrewAssignment>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.CrewAssignments)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeTraining>()
                .HasOne(et => et.TrainingEvent)
                .WithMany(te => te.EmployeeTrainings)
                .HasForeignKey(et => et.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeTraining>()
                .HasOne(et => et.Employee)
                .WithMany(e => e.EmployeeTrainings)
                .HasForeignKey(et => et.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TechnicalReport>()
                .HasOne(tr => tr.Aircraft)
                .WithMany(a => a.TechnicalReports)
                .HasForeignKey(tr => tr.AircraftRegistration)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TechnicalReport>()
                .HasOne(tr => tr.Employee)
                .WithMany(e => e.TechnicalReports)
                .HasForeignKey(tr => tr.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SparePart>()
                .HasOne(sp => sp.TechnicalReport)
                .WithMany(tr => tr.SpareParts)
                .HasForeignKey(sp => sp.TechnicalReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация новой сущности PilotReport
            modelBuilder.Entity<PilotReport>()
                .HasOne(r => r.Flight)
                .WithMany(f => f.PilotReports)
                .HasForeignKey(r => r.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PilotReport>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.PilotReports)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Qualification>()
                .HasOne(q => q.Employee)
                .WithMany(e => e.Qualifications)
                .HasForeignKey(q => q.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
