using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace Infrastructure.EntityConfigurations.ServiceConfigurations
{
    public class BabySeatConfiguration : IEntityTypeConfiguration<BabySeat>
    {
        public void Configure(EntityTypeBuilder<BabySeat> configuration)
        {
            configuration.HasMany(p => p.Images)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId);

            configuration.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}