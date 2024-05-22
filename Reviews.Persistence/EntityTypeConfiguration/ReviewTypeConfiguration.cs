using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reviews.Domain.Entities;
using Reviews.Persistence.Constants;

namespace Reviews.Persistence.EntityTypeConfiguration
{
    internal class ReviewTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable(TableNames.Reviews);
            builder.HasKey(x => x.Id);
        }
    }
}
