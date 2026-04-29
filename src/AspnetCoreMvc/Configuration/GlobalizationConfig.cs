using System.Globalization;

namespace AspnetCoreMvc.Configuration;

public static class GlobalizationConfig
{
    public static WebApplication UseGlobalizationConfig(this WebApplication app)
    {
        var defaultCulture = new CultureInfo("pt-BR");
        
        return app;
    }
}