using ManageUsers.Domain;
using ManageUsers.Domain.Enums;
using ManageUsers.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users
{
    public class DoctorTypeConfiguration:IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x => x.PhoneNumber)
                .HasConversion(x => x.Value, v => PhoneNumber.Create(v).Value);
            builder.ComplexProperty(c => c.FullName, b =>
            {
                b.IsRequired();
                b.Property(f => f.FirstName).HasColumnName("FirstName");
                b.Property(f => f.LastName).HasColumnName("LastName");
                b.Property(f => f.Patronymic).HasColumnName("Patronymic");
            });
            //builder.HasData(DoctorDomainErrors.Create(
            //    Guid.NewGuid(),
            //    FullName.Create("","","0").Value,
            //    DateTime.Now,
            //    "Doctor1Address",
            //    PhoneNumberDomainErrors.Create("+919367788755").Value,
            //    null,
            //    1,
            //    1,
            //    "High",
            //    new Guid("0f8fad5b-d9cb-469f-a165-70867728950d")));

        }
    }
}
