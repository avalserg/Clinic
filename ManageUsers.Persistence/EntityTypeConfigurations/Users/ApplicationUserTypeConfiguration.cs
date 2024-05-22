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

        builder.Navigation(r => r.ApplicationUserRole).AutoInclude(); ;

        builder.HasData(ApplicationUser.Create(
            new Guid("0f8fad5b-d9cb-469f-a165-70867728950a"),
            "Admin",
            "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            1
        ));
        builder.HasData(ApplicationUser.Create(
            new Guid("0f8fad5b-d9cb-469f-a165-70867728950d"),
            "Doctor1",
            "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            2

        ));
        builder.HasData(ApplicationUser.Create(
            new Guid("0f8fad5b-d9cb-469f-a165-70867728950b"),
            "Patient1",
            "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv",
            3

        ));


    }
}