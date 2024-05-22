using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PatientTickets.Domain;
using PatientTickets.Domain.Entities;
using PatientTickets.Persistence.Constants;

namespace PatientTickets.Persistence.EntityTypeConfiguration
{
    internal class PatientTicketTypeConfiguration : IEntityTypeConfiguration<PatientTicket>
    {
        public void Configure(EntityTypeBuilder<PatientTicket> builder)
        {
            builder.ToTable(TableNames.PatientTickets);
            builder.HasKey(x => x.Id);
        }
    }
}
