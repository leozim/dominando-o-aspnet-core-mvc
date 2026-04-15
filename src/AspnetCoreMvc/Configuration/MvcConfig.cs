using AspnetCoreMvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvc.Configuration;

public static class MvcConfig
{
    public static WebApplicationBuilder AddMvcConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
        });
        
        builder.Services.Configure<RazorViewEngineOptions>(options =>
        {
            options.AreaViewLocationFormats.Clear();
            options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/{1}/{0}.cshtml");
            options.AreaViewLocationFormats.Add("/Modulos/{2}/Views/Shared/{0}.cshtml");
            options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
        });
        
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString),
            ServiceLifetime.Scoped);
        
        builder.Services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(365);
            options.ExcludedHosts.Add("example.com");
            options.ExcludedHosts.Add("www.example.com");
        });
        
        return builder;
    }
}