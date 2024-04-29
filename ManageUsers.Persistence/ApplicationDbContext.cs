using System.Reflection;
using ManageUsers.Domain;
using Microsoft.EntityFrameworkCore;

namespace ManageUsers.Persistence;

public  class ApplicationDbContext : DbContext
{
    #region Users

    
   
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }



    #endregion

    

    

    #region Ef

 
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }


    #endregion
}