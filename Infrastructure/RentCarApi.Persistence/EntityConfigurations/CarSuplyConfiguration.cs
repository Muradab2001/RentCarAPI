using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace RentCarApi.Persistence.EntityConfigurations
{
    public class CarSuplyConfiguration : IEntityTypeConfiguration<CarSupply>
    {
        public void Configure(EntityTypeBuilder<CarSupply> configuration)
        {
            configuration.HasKey(cs => new { cs.CarId, cs.SupplyId });

            configuration.HasOne(bc => bc.Car)
            .WithMany(b => b.Supplies)
            .HasForeignKey(bc => bc.CarId);

            configuration.HasOne(bc => bc.Supply)
            .WithMany(c => c.Cars)
            .HasForeignKey(bc => bc.SupplyId);
        }
    }
}