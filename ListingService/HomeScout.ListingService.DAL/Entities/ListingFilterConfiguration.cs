using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeScout.ListingService.DAL.Entities
{
    public class ListingFilterConfiguration : IEntityTypeConfiguration<ListingFilter>
    {
        public void Configure(EntityTypeBuilder<ListingFilter> builder)
        {
            builder.HasKey(lf => new { lf.ListingId, lf.FilterId }); 

            builder.HasOne(lf => lf.Listing)
                   .WithMany(l => l.Filters)
                   .HasForeignKey(lf => lf.ListingId);

            builder.HasOne(lf => lf.Filter)
                   .WithMany(f => f.Listings)
                   .HasForeignKey(lf => lf.FilterId);
        }
    }
}
