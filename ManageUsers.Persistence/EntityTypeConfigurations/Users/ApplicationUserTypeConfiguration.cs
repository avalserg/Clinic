using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users;

public class ApplicationUserTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.ApplicationUserId);

        builder.Property(e => e.Login).HasMaxLength(50).IsRequired();
        builder.HasData(new ApplicationUser
        {
            ApplicationUserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a"),
            PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            Login = "Admin",
            ApplicationUserRole = ApplicationUserRoles.Admin

        }, 
            new ApplicationUser()
            {
                ApplicationUserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950d"),
                PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
                Login = "Doctor1",
                ApplicationUserRole = ApplicationUserRoles.Doctor
            }, new ApplicationUser()
            {
                ApplicationUserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950b"),
                PasswordHash = "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
                Login = "Patient1",
                ApplicationUserRole = ApplicationUserRoles.Patient
            });
    }
}