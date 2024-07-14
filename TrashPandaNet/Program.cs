using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TrashPandaNet.Data.DataBase;
using TrashPandaNet.Data.Models;
using TrashPandaNet.Data.Extensions;
using TrashPandaNet.Data.Repositories;
using TrashPandaNet.Logic.Mapping;
using TrashPandaNet.Logic.Services.Logging.Extensions;
using TrashPandaNet.Middleware;
using TrashPandaNet.Logic.Services;

namespace TrashPandaNet
{
    public class Program
    {
        private static IConfiguration Configuration { get; } =
            new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connection = Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connection, x => x.MigrationsAssembly("TrashPandaNet")))
                .AddUnitOfWork()
                .AddCustomRepository<Friend, FriendsRepository>()
                .AddCustomRepository<Message, MessageRepository>();

            builder.Services.AddIdentity<User, IdentityRole>(opts => 
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/";
            });

            builder.Services.AddSingleton<IUserGenerationService, UserGenerationservice>();

            builder.Logging.AddFileLogger();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
