using ManageUsers.Domain;
using Microsoft.EntityFrameworkCore;

namespace ManageUsers.Persistence;

public class ApplicationDbContext : DbContext
{
    #region Users



    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }




    #endregion





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