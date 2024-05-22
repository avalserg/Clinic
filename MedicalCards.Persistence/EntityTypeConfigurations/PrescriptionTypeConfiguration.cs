using MedicalCards.Domain;
using MedicalCards.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCards.Persistence.EntityTypeConfigurations
{
    public class PrescriptionTypeConfiguration:IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable(TableNames.Prescriptions);
            builder.HasKey(x => x.Id);
        }
    }
}
