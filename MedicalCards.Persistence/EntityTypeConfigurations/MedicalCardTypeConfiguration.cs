using MedicalCards.Domain;
using MedicalCards.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCards.Persistence.EntityTypeConfigurations
{
    public class MedicalCardTypeConfiguration : IEntityTypeConfiguration<MedicalCard>
    {
        public void Configure(EntityTypeBuilder<MedicalCard> builder)
        {
            builder.ToTable(TableNames.MedicalCards);
            builder.HasKey(x => x.Id);
            builder
                .HasMany(x => x.Appointments)
                .WithOne()
                .HasForeignKey(x => x.MedicalCardId);
        }
    }
}
