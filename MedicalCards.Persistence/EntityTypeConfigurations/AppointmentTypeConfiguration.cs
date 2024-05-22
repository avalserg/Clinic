using MedicalCards.Domain;
using MedicalCards.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCards.Persistence.EntityTypeConfigurations
{
    public class AppointmentTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable(TableNames.Appointments);
            builder.HasKey(x => x.Id);
            builder
                .HasMany(x => x.Prescriptions)
                .WithOne()
                .HasForeignKey(x => x.AppointmentId);
        }
    }
}
