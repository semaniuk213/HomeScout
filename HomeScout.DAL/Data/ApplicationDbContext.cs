using HomeScout.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<ListingFilter> ListingFilters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OwnerConfiguration());
            builder.ApplyConfiguration(new FilterConfiguration());
            builder.ApplyConfiguration(new ListingConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration());
            builder.ApplyConfiguration(new ListingFilterConfiguration());

            builder.Entity<Owner>().HasData(
                new Owner { Id = 1, Name = "Alice", Email = "alice@mail.com", Phone = "111-111-111" },
                new Owner { Id = 2, Name = "Bob", Email = "bob@mail.com", Phone = "222-222-222" },
                new Owner { Id = 3, Name = "Charlie", Email = "charlie@mail.com", Phone = "333-333-333" },
                new Owner { Id = 4, Name = "Diana", Email = "diana@mail.com", Phone = "444-444-444" }
            );

            builder.Entity<Listing>().HasData(
                new Listing { Id = 1, Title = "Cozy apartment downtown", Address = "123 Main St", City = "Kyiv", Price = 50000, Area = 50, Type = ListingType.Sale, CreatedAt = DateTime.UtcNow, OwnerId = 1 },
                new Listing { Id = 2, Title = "Modern loft", Address = "45 Freedom Ave", City = "Lviv", Price = 80000, Area = 70, Type = ListingType.Sale, CreatedAt = DateTime.UtcNow, OwnerId = 2 },
                new Listing { Id = 3, Title = "Small studio", Address = "12 Peace Rd", City = "Odesa", Price = 25000, Area = 30, Type = ListingType.Sale, CreatedAt = DateTime.UtcNow, OwnerId = 3 },
                new Listing { Id = 4, Title = "House with garden", Address = "789 Green Blvd", City = "Dnipro", Price = 120000, Area = 120, Type = ListingType.Sale, CreatedAt = DateTime.UtcNow, OwnerId = 4 }
            );

            builder.Entity<Photo>().HasData(
                new Photo { Id = 1, Url = "https://example.com/photo1.jpg", ListingId = 1 },
                new Photo { Id = 2, Url = "https://example.com/photo2.jpg", ListingId = 2 },
                new Photo { Id = 3, Url = "https://example.com/photo3.jpg", ListingId = 3 },
                new Photo { Id = 4, Url = "https://example.com/photo4.jpg", ListingId = 4 }
            );

            builder.Entity<Filter>().HasData(
                new Filter { Id = 1, Name = "Balcony" },
                new Filter { Id = 2, Name = "Parking" },
                new Filter { Id = 3, Name = "Elevator" },
                new Filter { Id = 4, Name = "Pet Friendly" }
            );

            builder.Entity<ListingFilter>().HasData(
                new ListingFilter { ListingId = 1, FilterId = 1 },
                new ListingFilter { ListingId = 1, FilterId = 2 },
                new ListingFilter { ListingId = 2, FilterId = 3 },
                new ListingFilter { ListingId = 3, FilterId = 4 }
            );

            base.OnModelCreating(builder);
        }
    }
}