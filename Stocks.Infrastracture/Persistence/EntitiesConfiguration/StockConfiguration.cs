using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocks.Domain.Entities;
using Stocks.Infrastracture.Persistence.ValueConverters;

namespace Stocks.Infrastracture.Persistence.EntitiesConfiguration;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Symbol)
            .HasColumnType("varchar(10)");

        builder.Property(s => s.Price)
            .HasColumnType("decimal(8, 2)");

        builder.Property(s => s.DateTime)
            .HasColumnType("datetime")
            .HasConversion<CustomDateTimeConverter>();

        builder.HasIndex(s => new { s.Symbol, s.DateTime })
            .IsUnique();
    }
}