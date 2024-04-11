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




try {
   

    var builder = WebApplication.CreateBuilder(args);

  


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


   

    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();

    builder.Services.AddSerilog();

    

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    ;

    var app = builder.Build();
    



    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment()) {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");
        app.UseHsts();
    }
    app.UseMiddleware<ErrorLogging>();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.UseMiddleware<RequestLogging>();
   

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

