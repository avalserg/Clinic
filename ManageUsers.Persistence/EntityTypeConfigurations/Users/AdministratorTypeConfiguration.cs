using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users;

public class AdministratorTypeConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        
        
        builder.HasData(new Administrator()
        {
            Id = 1,
            FirstName = "Admin",
            LastName = "Admin",
            Patronymic = "Admin",
            ApplicationUserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950a")
        });

    }
}