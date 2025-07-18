using Backend.Data;
using Backend.Data.Models;
using Backend.ExceptionHandlers;
using Backend.Services;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IOfferPlacementsService, OfferPlacementsService>();

builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ConflictExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityApiEndpoints<AppUser>()
 .AddRoles<IdentityRole<Guid>>()
 .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options
    .WithTitle("Job Apply Helper")
    .WithTheme(ScalarTheme.Saturn)
    .WithDarkMode());
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<AppUser>();

app.MapControllers();

app.Run();
