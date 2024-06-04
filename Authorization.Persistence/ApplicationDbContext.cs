using System.Reflection;
using Authorization.Domain;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Persistence;

public  class ApplicationDbContext : DbContext
{
    

    #region Auth

    public DbSet<RefreshToken> RefreshTokens { get; set; }

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