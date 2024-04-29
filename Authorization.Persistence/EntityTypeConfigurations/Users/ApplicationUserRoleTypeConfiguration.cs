using Authorization.Domain;
using Authorization.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserRoleTypeConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(e => e.ApplicationUserRoleId);

        builder.Property(e => e.Name).HasMaxLength(20).IsRequired();

        //builder.HasData(
        //    new ApplicationUserRole()
        //    {

        //        ApplicationUserRoleId = 1,
        //        Name = "Admin"
        //    },
        //    new ApplicationUserRole()
        //    {

        //        ApplicationUserRoleId = 2,
        //        Name = "Patient"
        //    },
        //    new ApplicationUserRole()
        //    {

        //        ApplicationUserRoleId = 3,
        //        Name = "Doctor"
        //    });
        var roles = Enum.GetValues<ApplicationUserRolesEnum>().Select(r => new ApplicationUserRole()
        {
            ApplicationUserRoleId = (int)r,
            Name = r.ToString()
        });
        builder.HasData(roles);
    }
}