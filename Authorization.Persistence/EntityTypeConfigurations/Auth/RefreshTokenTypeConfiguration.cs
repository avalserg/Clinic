using Authorization.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authorization.Persistence.EntityTypeConfigurations.Auth;

public class RefreshTokenTypeConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(e => e.RefreshTokenId);

        builder.Property(e => e.ApplicationUserId).IsRequired();

        builder.HasOne(e => e.ApplicationUser)
            .WithMany(e=>e.RefreshTokens)
            .HasForeignKey(e => e.ApplicationUserId);
    }
}