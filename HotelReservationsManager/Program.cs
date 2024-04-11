using HotelReservationsManager.Data;
using HotelReservationsManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<HotelReservationsManager.Models.Entities.User>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

using (var scope = app.Services.CreateScope())
{
    var Roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await Roles.RoleExistsAsync("Admin"))
    {
        await Roles.CreateAsync(new IdentityRole("Admin"));
        await Roles.CreateAsync(new IdentityRole("User"));
        User Admin = Activator.CreateInstance<User>();
        Admin.Active = true;
        Admin.HireDate = DateTime.Now;
        Admin.Email = "admin@admin.com";
        Admin.NormalizedEmail = "ADMIN@ADMIN.COM";
        Admin.UserName = "admin@admin.com";
        Admin.NormalizedUserName = "ADMIN@ADMIN.COM";
        Admin.FirstName = "Admin";
        Admin.LastName = "Admin";
        Admin.MiddleName = "Admin";
        Admin.EGN = "0123456789";
        Admin.PhoneNumber = "0123456789";
        Admin.EmailConfirmed = true;
        Admin.PasswordHash = new PasswordHasher<User>().HashPassword(Admin, "Admin1!");
        var Context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var UserStore = new UserStore<User>(Context);
        await UserStore.CreateAsync(Admin);
        UserManager<User> UserManager = scope.ServiceProvider.GetService<UserManager<User>>();
        await UserManager.AddToRolesAsync(Admin, new string[] { "User", "Admin" });
        await Context.SaveChangesAsync();
    }
}

app.Run();
