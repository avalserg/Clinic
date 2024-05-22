using ManageUsers.Domain;
using ManageUsers.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users;

public class AdministratorTypeConfiguration : IEntityTypeConfiguration<Administrator>
{
    public void Configure(EntityTypeBuilder<Administrator> builder)
    {
        builder.ToTable(TableNames.Administrators);
        builder.HasKey(x => x.Id);

        builder.ComplexProperty(c => c.FullName, b =>
        {
            b.IsRequired();
            b.Property(f => f.FirstName).HasColumnName("FirstName");
            b.Property(f => f.LastName).HasColumnName("LastName");
            b.Property(f => f.Patronymic).HasColumnName("Patronymic");
        });

        //builder.HasData(Administrator.Create(
        //    Guid.NewGuid(),
        //    FullName.Create("Admin", "Admin", "Admin").Value,
        //    new Guid("0f8fad5b-d9cb-469f-a165-70867728950a")
        //));
       
    }
}