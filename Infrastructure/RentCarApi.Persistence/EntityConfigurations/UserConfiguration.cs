using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> configuration)
        {
            configuration.HasOne(u => u.PersonalData)
                .WithOne(pd => pd.AppUser)
                .HasForeignKey<PersonalData>(pd => pd.AppUserId);

            configuration.HasOne(l => l.Location)
                .WithMany(c => c.Users)
                .HasForeignKey(l => l.LocationId);
        }
    }
}