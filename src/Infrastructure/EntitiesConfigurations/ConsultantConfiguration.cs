namespace Infrastructure.EntitiesConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class ConsultantConfiguration : IEntityTypeConfiguration<Consultant>
{
    public void Configure(EntityTypeBuilder<Consultant> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Skills).HasMaxLength(500);
        builder.HasMany(c => c.Missions)
               .WithOne(m => m.Consultant)
               .HasForeignKey(m => m.ConsultantId);
    }
}
