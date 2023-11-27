using Microsoft.EntityFrameworkCore;
using Store.Persistence.Contexts;
using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Users.Queries.GetUsers;
using Store.Application.Services.Users.Queries.GetRoles;
using Store.Application.Services.Users.Commands.RegisterUser;
using Store.Application.Services.Users.Commands.RemoveUser;
using Store.Application.Services.Users.Commands.UserSatusChange;
using Store.Application.Services.Users.Commands.EditUser;
using Microsoft.AspNetCore.Authentication.Cookies;
using Store.Application.Services.Users.Commands.UserLogin;
using System.Drawing;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.FacadePattern;
using Store.Application.Services.Products.Command.RemovCategory;
using Store.Application.Services.Common.FacadePattern;
using Store.Application.Services.HomePage.Commands.AddNewSlider;
using Store.Application.Services.HomePage.FacadPaterns;
using Store.Application.Services.CartServices;
using Store.Application.Services.Finance.Command.AddRequestPay;
using Store.Common.Roles;
using Store.Application.Services.Finance.Query.GetRequestPay;
using Store.Application.Services.Orders.Command.AddNewOrder;
using Store.Application.Services.Orders.Query.GetUserOrder;
using Store.Application.Services.Orders.Query.GetOrdersForAdmin;
using Store.Application.Services.Finance.Query.GetRequestPayForAdmin;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
    options.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
    options.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Authentication/Signin");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
    options.AccessDeniedPath = new PathString("/Authentication/Signin");
});

// Add services to the container.
#region MyServices
builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();
builder.Services.AddScoped<IGetUserService, GetUserServices>();
builder.Services.AddScoped<IGetRolesService, GetRolesService>();
builder.Services.AddScoped<IRegisterUserService, RegisterUserService>();
builder.Services.AddScoped<IRemoveUserService, RemoveUserService>();
builder.Services.AddScoped<IUserSatusChangeService, UserSatusChangeService>();
builder.Services.AddScoped<IEditUserService, EditUserService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<IAddRequestPay, AddRequestPay>();
builder.Services.AddScoped<IGetRequestPay, GetRequestPay>();
builder.Services.AddScoped<IAddNewOrder, AddNewOrder>();
builder.Services.AddScoped<IGetUserOrder, GetUserOrder>();
builder.Services.AddScoped<IGetOrdersForAdminService, GetOrdersForAdminService>();
builder.Services.AddScoped<IGetRequestPayForAdmin, GetRequestPayForAdmin>();
#endregion

#region MyFacadInjection
builder.Services.AddScoped<IProductFacad, ProductFacad>();
builder.Services.AddScoped<IProductForSiteFacad, ProductForSiteFacad>();
builder.Services.AddScoped<ICommon, CommonFacad>();
builder.Services.AddScoped<IHomePageFacad, HomePageFacad>();
#endregion
//var test = "data source = (local); Initial Catalog = XXXXXXX; Trusted_Connection = True; MultipleActiveResultSets = True; Encrypt = True; TrustServerCertificate = True";
string connectionString = "Data Source =.;Initial Catalog=BStore; Trusted_Connection = True; MultipleActiveResultSets = True; Encrypt = True; TrustServerCertificate = True";
builder.Services.AddDbContext<DataBaseContext>(options => { options.UseSqlServer(connectionString); });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
