namespace Infrastructure.EntitiesConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    public void Configure(EntityTypeBuilder<Mission> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.ClientName).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Status).HasConversion<string>();
    }
}
