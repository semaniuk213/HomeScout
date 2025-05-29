using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeScout.DAL.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.HasMany(u => u.Listings)
                   .WithOne(l => l.User)
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
