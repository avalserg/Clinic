using Microsoft.EntityFrameworkCore;
using PatientTickets.Domain;
using PatientTickets.Domain.Entities;

namespace PatientTickets.Persistence;

public  class ApplicationDbContext : DbContext
{
    #region Users

    
   
    public DbSet<PatientTicket> PatientTickets { get; set; }

    

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