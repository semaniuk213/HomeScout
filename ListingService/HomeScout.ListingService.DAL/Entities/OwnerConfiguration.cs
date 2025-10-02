using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeScout.ListingService.DAL.Entities
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Email)
                .HasMaxLength(100);

            builder.Property(o => o.Phone)
                .HasMaxLength(20);

            builder.HasMany(o => o.Listings)
                   .WithOne(l => l.Owner)
                   .HasForeignKey(l => l.OwnerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
