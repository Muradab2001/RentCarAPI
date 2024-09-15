using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace RentCarApi.Persistence.EntityConfigurations
{
    public class CarDiscountConfiguration : IEntityTypeConfiguration<CarDiscount>
    {
        public void Configure(EntityTypeBuilder<CarDiscount> builder)
        {
            builder
                .HasKey(cd => new
                {
                    cd.CarId,
                    cd.DiscountId
                });

            builder
                .HasOne(cd => cd.Car)
                .WithMany(c => c.CarDiscounts)
                .HasForeignKey(cd => cd.CarId);

            builder
                 .HasOne(cd => cd.Discount)
                 .WithMany(d => d.CarDiscounts)
                 .HasForeignKey(cd => cd.DiscountId);
        }
    }
}