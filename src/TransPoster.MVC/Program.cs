using System.Globalization;
using Microsoft.AspNetCore.Localization;
using TransPoster.Application;
using TransPoster.MVC.Extensions;
using TransPoster.MVC.Infra;
using CustomRequestCultureProvider = TransPoster.MVC.Infra.CustomRequestCultureProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.GetDataBaseDbContextConfig(builder.Configuration);
builder.Services.GetUserIdentityConfig();
var settings = builder.Services.GetApplicationSettings(builder.Configuration);
builder.Services.GetJwtAuthenticationConfig(settings);
builder.Services.GetInjectedRepositories();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
});
builder.Services.AddLocalization();
builder.Services.GetApplicationLayerConfig<AppConfiguration>();

builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new()
    {
        new CultureInfo("en-US"),
        new CultureInfo("he-IL"),
        new CultureInfo("fr-FR"),
        new CultureInfo("en-GB")
    };

    options.DefaultRequestCulture = new RequestCulture("he-IL");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new MyCustomRequestCultureProvider());
});

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

app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
