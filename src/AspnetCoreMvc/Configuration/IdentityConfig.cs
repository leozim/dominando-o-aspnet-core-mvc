using AspnetCoreMvc.Data;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreMvc.Configuration;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("PodeExcluirPermanentemente", policy => 
                policy.RequireRole("Admin"));
    
            options.AddPolicy("VerProdutos", policy => 
                policy.RequireClaim("Produtos", "VI"));
        });

        return builder;
    }
}