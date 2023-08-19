using Microsoft.EntityFrameworkCore;
using YouCode.Models.Context;

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.ConfigureServices(builder.Configuration);
*/// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<YouCodeContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "blogDetail",
        pattern: "Blog/BlogDetail/{id}",  // Replace "Blog" with your controller name if different
        defaults: new { controller = "Blog", action = "BlogDetail" }
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
