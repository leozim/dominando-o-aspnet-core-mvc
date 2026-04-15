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
        builder.Configuration
            .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile(
                $"appsettings.{builder.Environment.EnvironmentName}.json", 
                true, 
                true)
            .AddEnvironmentVariables();
        
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

    public static WebApplication UseMvcConfiguration(this WebApplication app)
    {
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

        app.UseAuthorization();

        app.MapAreaControllerRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
        app.MapAreaControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Gestao}/{action=Index}/{id?}");

/*app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");*/

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        return app;
    }
}