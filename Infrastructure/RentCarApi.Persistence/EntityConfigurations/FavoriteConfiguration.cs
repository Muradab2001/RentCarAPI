using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace Infrastructure.EntityConfigurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> configuration)
        {
            configuration.HasOne(f => f.User)
             .WithMany()
             .HasForeignKey(f => f.UserId)
             .IsRequired();

            configuration.HasOne(f => f.Car)
                .WithMany()
                .HasForeignKey(f => f.CarId)
                .IsRequired();
        }
    }
}