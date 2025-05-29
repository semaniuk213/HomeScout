using System.Text;
using FluentValidation;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.Profiles;
using HomeScout.BLL.Services;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Data;
using HomeScout.DAL.Entities;
using HomeScout.DAL.Repositories;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var jwtSettings = configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"] ?? string.Empty);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(FilterProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(PhotoProfile));
builder.Services.AddAutoMapper(typeof(ListingFilterProfile));
builder.Services.AddAutoMapper(typeof(ListingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<CreateFilterDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateFilterDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePhotoDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePhotoDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateListingFilterDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateListingFilterDto>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateListingDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateListingDto>();

builder.Services.AddScoped<IFilterService, FilterService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IListingFilterService, ListingFilterService>();
builder.Services.AddScoped<IListingService, ListingService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFilterRepository, FilterRepository>();
builder.Services.AddScoped<IListingFilterRepository, ListingFilterRepository>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeScout API", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    opt.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, [] }   
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
