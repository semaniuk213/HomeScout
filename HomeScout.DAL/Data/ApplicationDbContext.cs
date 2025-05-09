using HomeScout.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingFilter> ListingFilters { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Filter> Filters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FilterConfiguration());
            modelBuilder.ApplyConfiguration(new ListingConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
            modelBuilder.ApplyConfiguration(new ListingFilterConfiguration());

            modelBuilder.Entity<User>().HasData(
                new User { Id = "user1", UserName = "Alice Johnson", Email = "alice@mail.com", Password = "password1", Role = "User" },
                new User { Id = "user2", UserName = "Bob Smith", Email = "bob@mail.com", Password = "password2", Role = "User" },
                new User { Id = "user3", UserName = "Charlie Brown", Email = "charlie@mail.com", Password = "password3", Role = "User" },
                new User { Id = "user4", UserName = "Diana Prince", Email = "diana@mail.com", Password = "password4", Role = "User" },
                new User { Id = "user5", UserName = "Ethan Hunt", Email = "ethan@mail.com", Password = "password5", Role = "User" },
                new User { Id = "user6", UserName = "Fiona Glenanne", Email = "fiona@mail.com", Password = "password6", Role = "User" },
                new User { Id = "user7", UserName = "George Martin", Email = "george@mail.com", Password = "password7", Role = "User" },
                new User { Id = "admin1", UserName = "Admin Account", Email = "admin@mail.com", Password = "adminpass", Role = "Admin" }
            );

            modelBuilder.Entity<Listing>().HasData(
                new Listing { Id = 1, Title = "Cozy apartment downtown", Address = "123 Main St", City = "Kyiv", Price = 50000, Area = 50, Type = "Sale", CreatedAt = DateTime.Now, UserId = "user1" },
                new Listing { Id = 2, Title = "Modern loft", Address = "45 Freedom Ave", City = "Lviv", Price = 80000, Area = 70, Type = "Sale", CreatedAt = DateTime.Now, UserId = "user2" },
                new Listing { Id = 3, Title = "Small studio", Address = "12 Peace Rd", City = "Odesa", Price = 25000, Area = 30, Type = "Sale", CreatedAt = DateTime.Now, UserId = "user3" },
                new Listing { Id = 4, Title = "House with garden", Address = "789 Green Blvd", City = "Dnipro", Price = 120000, Area = 120, Type = "Sale", CreatedAt = DateTime.Now, UserId = "user4" },
                new Listing { Id = 5, Title = "Downtown office", Address = "65 Business St", City = "Kharkiv", Price = 700, Area = 100, Type = "Rent", CreatedAt = DateTime.Now, UserId = "user5" },
                new Listing { Id = 6, Title = "Studio for rent", Address = "33 Short St", City = "Kyiv", Price = 300, Area = 25, Type = "Rent", CreatedAt = DateTime.Now, UserId = "user6" },
                new Listing { Id = 7, Title = "Luxury apartment", Address = "99 Elite Way", City = "Lviv", Price = 150000, Area = 150, Type = "Sale", CreatedAt = DateTime.Now, UserId = "user7" },
                new Listing { Id = 8, Title = "Cheap room", Address = "11 Budget Ln", City = "Odesa", Price = 150, Area = 20, Type = "Rent", CreatedAt = DateTime.Now, UserId = "user1" }
            );

            modelBuilder.Entity<Photo>().HasData(
                new Photo { Id = 1, Url = "https://example.com/photo1.jpg", ListingId = 1 },
                new Photo { Id = 2, Url = "https://example.com/photo2.jpg", ListingId = 2 },
                new Photo { Id = 3, Url = "https://example.com/photo3.jpg", ListingId = 3 },
                new Photo { Id = 4, Url = "https://example.com/photo4.jpg", ListingId = 4 },
                new Photo { Id = 5, Url = "https://example.com/photo5.jpg", ListingId = 5 },
                new Photo { Id = 6, Url = "https://example.com/photo6.jpg", ListingId = 6 },
                new Photo { Id = 7, Url = "https://example.com/photo7.jpg", ListingId = 7 },
                new Photo { Id = 8, Url = "https://example.com/photo8.jpg", ListingId = 8 }
            );

            modelBuilder.Entity<Filter>().HasData(
                new Filter { Id = 1, Name = "Balcony" },
                new Filter { Id = 2, Name = "Parking" },
                new Filter { Id = 3, Name = "Elevator" },
                new Filter { Id = 4, Name = "Pet Friendly" },
                new Filter { Id = 5, Name = "Furnished" },
                new Filter { Id = 6, Name = "Air Conditioning" },
                new Filter { Id = 7, Name = "Garden" },
                new Filter { Id = 8, Name = "Pool" }
            );

            modelBuilder.Entity<ListingFilter>().HasData(
                new ListingFilter { ListingId = 1, FilterId = 1 },
                new ListingFilter { ListingId = 1, FilterId = 2 },
                new ListingFilter { ListingId = 2, FilterId = 3 },
                new ListingFilter { ListingId = 3, FilterId = 4 },
                new ListingFilter { ListingId = 4, FilterId = 5 },
                new ListingFilter { ListingId = 5, FilterId = 6 },
                new ListingFilter { ListingId = 6, FilterId = 7 },
                new ListingFilter { ListingId = 7, FilterId = 8 }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
