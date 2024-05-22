using Microsoft.EntityFrameworkCore;
using Reviews.Domain.Entities;

namespace Reviews.Persistence;

public  class ApplicationDbContext : DbContext
{
    #region Users

    
   
    public DbSet<Review> Reviews { get; set; }

    

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