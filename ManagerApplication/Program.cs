using ManagerApplication.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ManagerAppContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Index"; // ��������� ���� � �������� �����
        options.AccessDeniedPath = "/Home/Index"; // ��������� ���� � �������� ������� ��������
    });

builder.Services.AddAuthorization();

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

app.UseAuthentication(); // ��������� �������������� ����� ������������

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "accessdenied",
        pattern: "AccessDenied",
        defaults: new { controller = "Home", action = "AccessDenied" });


    // Middleware ��� ��������� �������������� �������
    app.Use(async (context, next) =>
    {
        if (!context.Request.Path.StartsWithSegments("/AccessDenied"))
        {
            await next();

            if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
            {
                // �������������� �� �������� accessdenied
                context.Response.Redirect("/AccessDenied");
            }
        }
        else
        {
            await next();
        }
    });
});

app.Run();
