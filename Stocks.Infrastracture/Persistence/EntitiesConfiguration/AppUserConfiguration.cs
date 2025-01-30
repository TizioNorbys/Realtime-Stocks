using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Entities;

namespace Stocks.Infrastracture.Persistence.EntitiesConfiguration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.FirstName)
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .HasMaxLength(50);

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}