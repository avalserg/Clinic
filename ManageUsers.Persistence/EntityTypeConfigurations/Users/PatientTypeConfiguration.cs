using ManageUsers.Domain;
using ManageUsers.Domain.ValueObjects;
using ManageUsers.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageUsers.Persistence.EntityTypeConfigurations.Users
{
    public class PatientTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {


            builder.ToTable(TableNames.Patients);
            builder.HasKey(p => p.Id);

            builder.ComplexProperty(c => c.FullName, b =>
            {
                b.IsRequired();
                b.Property(f => f.FirstName).HasColumnName("FirstName");
                b.Property(f => f.LastName).HasColumnName("LastName");
                b.Property(f => f.Patronymic).HasColumnName("Patronymic");
            });
            builder.HasIndex(p => p.PassportNumber).IsUnique();
            builder.Property(p => p.PassportNumber).IsRequired().HasMaxLength(9);
            builder
                .Property(x => x.PhoneNumber)
                .HasConversion(x => x.Value, v => PhoneNumber.Create(v).Value);

        }
    }
}
