using Microsoft.EntityFrameworkCore;
using WWEManagement.Data.Models;

namespace WWEManagement.Data.Data;

public class WWEManagementDbContext : DbContext
{
    public WWEManagementDbContext(
        DbContextOptions<WWEManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees =>
        Set<Employee>();

    public DbSet<Wrestler> Wrestlers =>
        Set<Wrestler>();

    public DbSet<Event> Events =>
        Set<Event>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees");

            entity.HasKey(e => e.EmployeeId);

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.HireDate)
                .HasColumnType("date");

            entity.Property(e => e.IsActive)
                .IsRequired();
        });

        modelBuilder.Entity<Wrestler>(entity =>
        {
            entity.ToTable("Wrestlers");

            entity.HasKey(w => w.WrestlerId);

            entity.Property(w => w.RingName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(w => w.WeightClass)
                .HasMaxLength(50);

            entity.Property(w => w.DebutDate)
                .HasColumnType("date");

            entity.Property(w => w.IsActive)
                .IsRequired();

            entity.HasIndex(w => w.EmployeeId)
                .IsUnique();

            entity.HasOne(w => w.Employee)
                .WithOne(e => e.Wrestler)
                .HasForeignKey<Wrestler>(
                    w => w.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");

            entity.HasKey(e => e.EventId);

            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.EventDate)
                .HasColumnType("datetime2");

            entity.Property(e => e.Venue)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.State)
                .HasMaxLength(50);

            entity.Property(e => e.Status)
                .HasMaxLength(30)
                .IsRequired();
        });
    }
}