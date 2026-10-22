using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VinylHub.Application.Interfaces;
using VinylHub.Application.Interfaces.Services;
using VinylHub.Application.Services;
using VinylHub.Infrastructure;
using VinylHub.Infrastructure.Data;
using VinylHub.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// db context
string? connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<VinylHubDbContext>(x => x.UseSqlServer(connectionString));

// identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<VinylHubDbContext>();

// autorisatie policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageVinyls", builder => builder.RequireClaim("IsAdmin"));
});

// services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IVinylService, VinylService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<FileUploadService>();

var app = builder.Build();

// seeding van rollen
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    SeedData.Initialize(userManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
