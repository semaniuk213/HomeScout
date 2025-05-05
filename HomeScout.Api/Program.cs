using FluentValidation;
using HomeScout.BLL.DTOs;
using HomeScout.BLL.DTOs.Validators;
using HomeScout.BLL.Profiles;
using HomeScout.BLL.Services;
using HomeScout.BLL.Services.Interfaces;
using HomeScout.DAL.Data;
using HomeScout.DAL.Repositories;
using HomeScout.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(FilterProfile));

builder.Services.AddValidatorsFromAssemblyContaining<CreateFilterDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateFilterDto>();

builder.Services.AddScoped<IFilterService, FilterService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFilterRepository, FilterRepository>();
builder.Services.AddScoped<IListingFilterRepository, ListingFilterRepository>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
