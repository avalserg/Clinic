using Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.ApplicationUserId);

        builder.Property(e => e.Login).HasMaxLength(50).IsRequired();

        builder
            .HasOne(e => e.ApplicationUserRole)
            .WithOne(e => e.ApplicationUser)
            .HasForeignKey<ApplicationUserRole>(e => e.ApplicationUserRoleId);
        
        builder.Navigation(e => e.ApplicationUserRole).AutoInclude();
    }
}