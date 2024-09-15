﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentCarApi.Domain.Models;

namespace Infrastructure.EntityConfigurations.Image
{
    public class BabySeatImageConfiguration : IEntityTypeConfiguration<Image<BabySeat>>
    {
        public void Configure(EntityTypeBuilder<Image<BabySeat>> configuration)
        {
            configuration.ToTable("babySeat_images");
            configuration.HasKey(x => x.Id);
            configuration.Property(b => b.Id).HasColumnName("id");
            configuration.Property(x => x.ImageUrl).IsRequired().HasColumnName("image_url");
            configuration.Property(x => x.ItemId).IsRequired().HasColumnName("item_id");
            configuration.HasOne(x => x.Item)
                .WithMany(i => i.Images)
                .HasForeignKey(x => x.ItemId);
        }
    }
}