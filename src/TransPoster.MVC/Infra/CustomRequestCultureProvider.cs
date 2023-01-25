using Microsoft.AspNetCore.Localization;

namespace TransPoster.MVC.Infra;
public class MyCustomRequestCultureProvider : RequestCultureProvider
{
    public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        await Task.Yield();
        return new ProviderCultureResult("he-IL");
    }
}

