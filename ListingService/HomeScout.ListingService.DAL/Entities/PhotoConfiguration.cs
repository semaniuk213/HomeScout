using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeScout.ListingService.DAL.Entities
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Url)
                   .IsRequired()
                   .HasMaxLength(400);

            builder.HasOne(p => p.Listing)
                   .WithMany(l => l.Photos)
                   .HasForeignKey(p => p.ListingId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
