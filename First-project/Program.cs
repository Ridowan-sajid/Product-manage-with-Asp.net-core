using First.DAL.Repository;
using First.DAL.Repository.IRepository;
using First_project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using First.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//we are registering here the db services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//we can add "" inside <IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true) then user must have confirm their email
//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();


//We are trying to extend user that's why we will changed the DefaulIdentity service
//If we try to add Identity we need to add IdentityRole also. 
builder.Services.AddIdentity<IdentityUser,IdentityRole>().
    AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//AddDefaultTokenProviders() added because we have to take care of token though we extend user model 



////////// TO set Some in build Path. VVI(Must add this after AddIdentity<>)///////
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
///////////////////////



//Add Razor pages
builder.Services.AddRazorPages();


//Add CategoryRepo Sevice 
builder.Services.AddScoped<ICategoryRepo,CategoryRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();

///EmailSender service added
builder.Services.AddScoped<IEmailSender,EmailSender>();

////////////////////

var app = builder.Build();

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
//we have to use it to use Identity before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");


app.Run();


//In an ASP.NET application, the Program.cs file is the entry point of the application.
//we will register many services here with the help of builder