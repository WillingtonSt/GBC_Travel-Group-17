using GBC_Travel_Group_32.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Identity.Client;
using GBC_Travel_Group_32.Models;
using COMP2139_Labs.Services;
using GBC_Travel_Group_32.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Serilog;
using Serilog.Filters;
using System.Net;




try {
   

    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Information()
          .Filter.ByIncludingOnly(logEvent => logEvent.Properties.ContainsKey("Message"))
          .WriteTo.Console()
          .CreateLogger();

    builder.Services.AddSerilog();

    builder.Services.AddDbContext<ApplicationDBContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




    builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDBContext>()
        .AddRoles<IdentityRole>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();


    builder.Services.AddSingleton<IEmailSender, EmailSender>();

    builder.Services.AddTransient<EmailSender>();

    builder.Services.AddScoped<Listing>();

    builder.Services.AddTransient<LoggingFilter>();
  

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

   

 
    

    var app = builder.Build();

    
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
  
    app.UseAuthorization();

   
    app.UseMiddleware<RequestLogging>();

    app.UseExceptionHandler(errorApp => {

     

        errorApp.Run(async context => {


            context.Response.Redirect($"/error/{context.Response.StatusCode}");

            


        });

    });


    app.UseMiddleware<ErrorLogging>();


    app.UseStatusCodePages(async context => {

       

        if (context.HttpContext.Response.StatusCode == 404) {
            context.HttpContext.Response.Redirect("/error/404");
        }
        else if (context.HttpContext.Response.StatusCode == 500) {
            context.HttpContext.Response.Redirect("/error/500");
        } else {
            context.HttpContext.Response.Redirect($"/error/{context.HttpContext.Response.StatusCode}");
        }
    });

    

  


    app.UseHsts();


   







    app.UseAuthentication();
   

    app.MapRazorPages();

   
   

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    using (var scope = app.Services.CreateScope()) {

        var roleManager =
            scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "Traveler" };


        foreach (var role in roles) {

            if (!await roleManager.RoleExistsAsync(role)) {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

    }



    using (var scope = app.Services.CreateScope()) {

        var userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        string email = "admin@admin.com";
        string password = "@Admin1234";
        string firstName = "Admin";
        string lastName = "John";

        if (await userManager.FindByEmailAsync(email) == null) {

            var user = new User();
            user.UserName = email;
            user.Email = email;
            user.EmailConfirmed = true;

            user.FirstName = firstName;
            user.LastName = lastName;
            user.HotelLoyaltyID = null;
            user.FrequentFlyerNumber = null;
            user.ProfilePicture = null;

            await userManager.CreateAsync(user, password);

            await userManager.AddToRoleAsync(user, "Admin");
        }





    }



   app.Run();

}
catch (Exception ex) 
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally 
{
    Log.CloseAndFlush();
}

