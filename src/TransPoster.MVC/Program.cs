using TransPoster.Application;
using TransPoster.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.GetDataBaseDbContextConfig(builder.Configuration);
builder.Services.GetUserIdentityConfig();
var settings = builder.Services.GetApplicationSettings(builder.Configuration);
builder.Services.GetJwtAuthenticationConfig(settings);
builder.Services.GetInjectedRepositories();
builder.Services.AddLocalization();
builder.Services.GetApplicationLayerConfig<AppConfiguration>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
{
    options.ViewLocationFormats.Add("/{0}.cshtml");
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
