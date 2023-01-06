using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpartaToDo.Data;
using SpartaToDo.Services;

namespace SpartaToDo {
    public class Program {
        public static void Main( string[] args ) {
            var builder = WebApplication.CreateBuilder( args );

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" ) ?? throw new InvalidOperationException( "Connection string 'DefaultConnection' not found." );
            builder.Services.AddDbContext<SpartaToDoContext>( options =>
                options.UseSqlServer( connectionString ) );
            builder.Services.AddScoped<IToDoService, ToDoService>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>( options => options.SignIn.RequireConfirmedAccount = true )
                .AddEntityFrameworkStores<SpartaToDoContext>();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            using ( var scope = app.Services.CreateScope() ) {
                SeedData.Initialise( scope.ServiceProvider );
            }

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() ) {
                app.UseMigrationsEndPoint();
            }
            else {
                app.UseExceptionHandler( "/Home/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}" );
            app.MapRazorPages();

            app.Run();
        }
    }
}