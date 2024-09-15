using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace Infrastructure.EntityConfigurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> configuration)
        {
            configuration.Property(e => e.TransmissionType)
               .HasConversion<string>();

            configuration.Property(e => e.FuelType)
              .HasConversion<string>();

            configuration.HasOne(l => l.Location)
                .WithMany(c => c.Cars)
                .HasForeignKey(l => l.LocationId);

            configuration.HasMany(p => p.Images)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId);

            configuration.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}