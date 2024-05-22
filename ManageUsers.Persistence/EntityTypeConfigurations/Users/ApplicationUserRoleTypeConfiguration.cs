using ManageUsers.Domain;
using ManageUsers.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users
{
    public class ApplicationUserRoleTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.ToTable(TableNames.ApplicationUserRoles);
            builder.HasKey(k => k.ApplicationUserRoleId);
            builder.HasData(
             ApplicationUserRole.Create(1, "Admin")
             );
            builder.HasData(
             ApplicationUserRole.Create(2, "Doctor")
              );
            builder.HasData(
             ApplicationUserRole.Create(3, "Patient")
             );
        }
    }
}
