using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RehearsalHub.Data;
using RehearsalHub.Data.Models;
using RehearsalHub.Services;
using RehearsalHub.Services.Data.Bands;
using RehearsalHub.Services.Data.Invitation;
using RehearsalHub.Services.Data.Notifications;
using RehearsalHub.Services.Data.Rehearsals;
using RehearsalHub.Services.Data.Setlists;
using RehearsalHub.Services.Data.Songs;
using RehearsalHub.Services.Data.Users;

namespace RehearsalHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
                options.Password.RequireDigit = true; 
                options.Password.RequireLowercase = true; 
                options.Password.RequireNonAlphanumeric = true; 
                options.Password.RequireUppercase = true; 
                options.Password.RequiredLength = 10; 
                options.Password.RequiredUniqueChars = 1; 

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true; 
                options.SignIn.RequireConfirmedAccount = false; 
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IInvitationService, InvitationService>();
            builder.Services.AddScoped<IBandService, BandService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISongService, SongService>();
            builder.Services.AddScoped<ISetlistService, SetlistService>();
            builder.Services.AddScoped<IRehearsalService, RehearsalService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<NotificationsHub>("/notificationsHub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}