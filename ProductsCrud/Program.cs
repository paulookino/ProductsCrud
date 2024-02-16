using Business.Interfaces.Repositories;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "ProductsCookie";
        config.LoginPath = "/User/Login";
    });

builder.Services.AddAuthorization();

string suaConnectionString = "SuaConnectionStringAqui";

builder.Services.AddScoped<IProductRepository>(provider => new ProductRepository(suaConnectionString));
builder.Services.AddScoped<IUserRepository>(provider => new UserRepository(suaConnectionString));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
