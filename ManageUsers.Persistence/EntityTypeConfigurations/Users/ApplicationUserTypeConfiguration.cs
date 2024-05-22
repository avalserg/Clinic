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
        builder.HasData(ApplicationUser.Create(
            new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"),
            "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            "Admin",
            ApplicationUserRolesEnum.Admin
        ));
        builder.HasData(ApplicationUser.Create(
            new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"),
            "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            "Doctor1",
            ApplicationUserRolesEnum.Doctor 

        ));
        builder.HasData(ApplicationUser.Create(
            new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"),
            "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            "Patient1",
           ApplicationUserRolesEnum.Patient 

        ));
      
           
    }
}