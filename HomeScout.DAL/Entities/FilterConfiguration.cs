using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeScout.DAL.Entities
{
    public class FilterConfiguration : IEntityTypeConfiguration<Filter>
    {
        public void Configure(EntityTypeBuilder<Filter> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(f => f.Name)
                   .IsUnique();

            builder.HasMany(f => f.Listings)
                   .WithOne(lf => lf.Filter)
                   .HasForeignKey(lf => lf.FilterId);
        }
    }
}
