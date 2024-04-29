using Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserRoleTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(e => e.ApplicationUserRoleId);

        builder.Property(e => e.Name).HasMaxLength(20).IsRequired();

        builder.HasOne(e => e.ApplicationUser)
            .WithOne(e => e.ApplicationUserRole)
            .HasForeignKey<ApplicationUser>(e => e.RoleId);
    }
}