using HomeScout.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeScout.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingFilter> ListingFilters { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Filter> Filters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FilterConfiguration());
            builder.ApplyConfiguration(new ListingConfiguration());
            builder.ApplyConfiguration(new PhotoConfiguration());
            builder.ApplyConfiguration(new ListingFilterConfiguration());

            builder.Entity<User>().OwnsMany(u => u.RefreshTokens);

            var roleUser = new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" };
            var roleAdmin = new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" };

            builder.Entity<IdentityRole<Guid>>().HasData(roleUser, roleAdmin);

            // --- Users ---
            var passwordHasher = new PasswordHasher<User>();

            var userIds = new[]
            {
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Guid.Parse("77777777-7777-7777-7777-777777777777"),
                Guid.Parse("88888888-8888-8888-8888-888888888888"),
            };

            var users = new List<User>
            {
                new User { Id = userIds[0], UserName = "alice@mail.com", Email = "alice@mail.com", NormalizedEmail = "ALICE@MAIL.COM", NormalizedUserName = "ALICE@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[1], UserName = "bob@mail.com", Email = "bob@mail.com", NormalizedEmail = "BOB@MAIL.COM", NormalizedUserName = "BOB@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[2], UserName = "charlie@mail.com", Email = "charlie@mail.com", NormalizedEmail = "CHARLIE@MAIL.COM", NormalizedUserName = "CHARLIE@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[3], UserName = "diana@mail.com", Email = "diana@mail.com", NormalizedEmail = "DIANA@MAIL.COM", NormalizedUserName = "DIANA@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[4], UserName = "ethan@mail.com", Email = "ethan@mail.com", NormalizedEmail = "ETHAN@MAIL.COM", NormalizedUserName = "ETHAN@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[5], UserName = "fiona@mail.com", Email = "fiona@mail.com", NormalizedEmail = "FIONA@MAIL.COM", NormalizedUserName = "FIONA@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[6], UserName = "george@mail.com", Email = "george@mail.com", NormalizedEmail = "GEORGE@MAIL.COM", NormalizedUserName = "GEORGE@MAIL.COM", EmailConfirmed = true },
                new User { Id = userIds[7], UserName = "admin@mail.com", Email = "admin@mail.com", NormalizedEmail = "ADMIN@MAIL.COM", NormalizedUserName = "ADMIN@MAIL.COM", EmailConfirmed = true }
            };

            foreach (var user in users)
            {
                string password = user.UserName == "admin@mail.com" ? "Admin@1234" : "password";
                user.PasswordHash = passwordHasher.HashPassword(user, password);
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
            }

            builder.Entity<User>().HasData(users);

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid> { UserId = users[7].Id, RoleId = roleAdmin.Id }
            );

            for (int i = 0; i < 7; i++)
            {
                builder.Entity<IdentityUserRole<Guid>>().HasData(
                    new IdentityUserRole<Guid> { UserId = users[i].Id, RoleId = roleUser.Id }
                );
            }

            builder.Entity<Listing>().HasData(
                new Listing { Id = 1, Title = "Cozy apartment downtown", Address = "123 Main St", City = "Kyiv", Price = 50000, Area = 50, Type = ListingType.Sale, CreatedAt = DateTime.Now, UserId = userIds[0] },
                new Listing { Id = 2, Title = "Modern loft", Address = "45 Freedom Ave", City = "Lviv", Price = 80000, Area = 70, Type = ListingType.Sale, CreatedAt = DateTime.Now, UserId = userIds[1] },
                new Listing { Id = 3, Title = "Small studio", Address = "12 Peace Rd", City = "Odesa", Price = 25000, Area = 30, Type = ListingType.Sale, CreatedAt = DateTime.Now, UserId = userIds[2] },
                new Listing { Id = 4, Title = "House with garden", Address = "789 Green Blvd", City = "Dnipro", Price = 120000, Area = 120, Type = ListingType.Sale, CreatedAt = DateTime.Now, UserId = userIds[3] },
                new Listing { Id = 5, Title = "Downtown office", Address = "65 Business St", City = "Kharkiv", Price = 700, Area = 100, Type = ListingType.Rent, CreatedAt = DateTime.Now, UserId = userIds[4] },
                new Listing { Id = 6, Title = "Studio for rent", Address = "33 Short St", City = "Kyiv", Price = 300, Area = 25, Type = ListingType.Rent, CreatedAt = DateTime.Now, UserId = userIds[5] },
                new Listing { Id = 7, Title = "Luxury apartment", Address = "99 Elite Way", City = "Lviv", Price = 150000, Area = 150, Type = ListingType.Sale, CreatedAt = DateTime.Now, UserId = userIds[6] },
                new Listing { Id = 8, Title = "Cheap room", Address = "11 Budget Ln", City = "Odesa", Price = 150, Area = 20, Type = ListingType.Rent, CreatedAt = DateTime.Now, UserId = userIds[0] }
            );

            builder.Entity<Photo>().HasData(
                new Photo { Id = 1, Url = "https://example.com/photo1.jpg", ListingId = 1 },
                new Photo { Id = 2, Url = "https://example.com/photo2.jpg", ListingId = 2 },
                new Photo { Id = 3, Url = "https://example.com/photo3.jpg", ListingId = 3 },
                new Photo { Id = 4, Url = "https://example.com/photo4.jpg", ListingId = 4 },
                new Photo { Id = 5, Url = "https://example.com/photo5.jpg", ListingId = 5 },
                new Photo { Id = 6, Url = "https://example.com/photo6.jpg", ListingId = 6 },
                new Photo { Id = 7, Url = "https://example.com/photo7.jpg", ListingId = 7 },
                new Photo { Id = 8, Url = "https://example.com/photo8.jpg", ListingId = 8 }
            );

            builder.Entity<Filter>().HasData(
                new Filter { Id = 1, Name = "Balcony" },
                new Filter { Id = 2, Name = "Parking" },
                new Filter { Id = 3, Name = "Elevator" },
                new Filter { Id = 4, Name = "Pet Friendly" },
                new Filter { Id = 5, Name = "Furnished" },
                new Filter { Id = 6, Name = "Air Conditioning" },
                new Filter { Id = 7, Name = "Garden" },
                new Filter { Id = 8, Name = "Pool" }
            );

            builder.Entity<ListingFilter>().HasData(
                new ListingFilter { ListingId = 1, FilterId = 1 },
                new ListingFilter { ListingId = 1, FilterId = 2 },
                new ListingFilter { ListingId = 2, FilterId = 3 },
                new ListingFilter { ListingId = 3, FilterId = 4 },
                new ListingFilter { ListingId = 4, FilterId = 5 },
                new ListingFilter { ListingId = 5, FilterId = 6 },
                new ListingFilter { ListingId = 6, FilterId = 7 },
                new ListingFilter { ListingId = 7, FilterId = 8 }
            );

            base.OnModelCreating(builder);
        }

    }
}
