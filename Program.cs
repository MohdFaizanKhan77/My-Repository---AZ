using EmolyeePortal.Data;
using EmolyeePortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace EmolyeePortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            //Add AplpicationDbContext and configure SQl serverconnection string
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString(" ECommerDbConnection")));


            builder.Services.AddDbContext<EmolyeePortalContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("EmolyeePortalContextConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EmolyeePortalContext>();

            builder.Services.AddSingleton<IEmailSender, EmailSender>();


            builder.Services.AddScoped<EmployeeService>();

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=List}/{id?}")
                .WithStaticAssets();

            app.Run();

        }

    }
}















