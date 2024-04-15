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




    builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
        .AddEntityFrameworkStores<ApplicationDBContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();
       


    builder.Services.AddSingleton<IEmailSender, EmailSender>();

    builder.Services.AddTransient<EmailSender>();

    builder.Services.AddScoped<Listing>();

    builder.Services.AddTransient<LoggingFilter>();
  

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

   

 
    

    var app = builder.Build();



    using var scope = app.Services.CreateScope();
    var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

    try {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SuperSeedRoleAsync(userManager, roleManager);

    } catch (Exception ex) {

        var logger = loggerFactory.CreateLogger<Listing>();
        logger.LogError(ex, "An error occured when attempting to seed the roles for the system.");
    }




    app.UseHttpsRedirection();

   

    app.UseStaticFiles();

    

    app.UseRouting();


   


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

    app.UseAuthorization();

    app.MapRazorPages();

    


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


    



    

   
   

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

