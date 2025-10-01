using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeScout.DAL.Entities
{
    public class ListingConfiguration : IEntityTypeConfiguration<Listing>
    {
        public void Configure(EntityTypeBuilder<Listing> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(l => l.Description)
                   .HasMaxLength(1000);

            builder.Property(l => l.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(l => l.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(l => l.Area)
                   .IsRequired();

            builder.Property(l => l.Type)
                   .IsRequired()
                   .HasConversion<string>() 
                   .HasMaxLength(50);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Listing_Type", "[Type] IN ('Rent', 'Sale')");
            });

            builder.Property(l => l.CreatedAt)
                   .IsRequired();

            builder.HasOne(l => l.Owner)
                   .WithMany(o => o.Listings)
                   .HasForeignKey(l => l.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Photos)
                   .WithOne(p => p.Listing)
                   .HasForeignKey(p => p.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Filters)
                   .WithOne(lf => lf.Listing)
                   .HasForeignKey(lf => lf.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
