using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RentCarApi.Domain.Common;
using RentCarApi.Domain.Models;
using RentCarApi.Domain.Models.Base;
using System.Reflection;

namespace RentCarApi.Persistence.Context;
public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<Brand> Brends { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<CompanyData> CompanyDatas { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonalData> PersonalDatas { get; set; }
    public DbSet<Supply> Supplies { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<BabySeat> BabySeats { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<CarSupply> CarSupplies { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<FAQ> FAQs { get; set; }

    public DbSet<PromoCode> PromoCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AutoUpdateCreatedAndModifiedValue();

        return base.SaveChangesAsync(cancellationToken);
    }
    private void AutoUpdateCreatedAndModifiedValue()
    {
        var entries = ChangeTracker.Entries().Where(e => (e.Entity is BaseEntity/* || e.Entity is AppUser*/)
        && (e.State == EntityState.Added
        || e.State == EntityState.Modified));
        if (entries.Any(x => x.Entity is BaseEntity)) UpdateDateTimesForBaseEntity(entries);
    }

    private static void UpdateDateTimesForBaseEntity(IEnumerable<EntityEntry> entries)
    {
        foreach (var entityEntry in entries.Where(x => x.Entity is BaseEntity))
        {
            ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }
    }
}