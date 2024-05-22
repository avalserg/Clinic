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
        builder.HasOne(e => e.ApplicationUserRole).WithMany(e=>e.ApplicationUsers);
        builder.Navigation(e => e.ApplicationUserRole).AutoInclude();
        builder.HasData(new ApplicationUser
        {
            ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"),
            PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            Login = "Admin",
            ApplicationUserRoleId = 1
        },
            new ApplicationUser()
            {
                ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"),
                PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
                Login = "Patient1",
                ApplicationUserRoleId = 2
            },
        new ApplicationUser()
        {ApplicationUserId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"),
            PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            Login = "Doctor1",
            ApplicationUserRoleId = 3
        });
    }
}
