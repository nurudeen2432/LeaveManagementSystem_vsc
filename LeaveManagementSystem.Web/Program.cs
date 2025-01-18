using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using LeaveManagementSystem.Application.Services.Email;
using LeaveManagementSystem.Application;
using Serilog;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// -> now we setup a profile which is a configuration file that tells automapper what data type
// -> it should be looking out for i.e We want to map between the leave type object.
// -> we want to Map the LeaveType DataModel to the IndexVM class




builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
    
builder.Services.AddControllersWithViews();



//Application project
ApplicationServiceRegistrations.AddApplicationServices(builder.Services);

DataServiceRegistration.AddDataServices(builder.Services, builder.Configuration);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminSupervisorOnly", policy => {
    policy.RequireRole(Roles.Administrator, Roles.Supervisor);

});


builder.Host.UseSerilog((ctx, config)=>{
    config.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration);
});



builder.Services.AddHttpContextAccessor();

//I need a new client anytime Email should be dispatched


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

app.Run();
