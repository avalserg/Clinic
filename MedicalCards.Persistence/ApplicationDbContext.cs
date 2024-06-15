using MedicalCards.Domain;
using Microsoft.EntityFrameworkCore;

namespace MedicalCards.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalCard> MedicalCards { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    #region Ef

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }

    #endregion
}