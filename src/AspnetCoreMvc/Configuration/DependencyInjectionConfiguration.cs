using AspnetCoreMvc.Services;

namespace AspnetCoreMvc.Configuration;

public static class DependencyInjectionConfiguration
{
    public static WebApplicationBuilder AddDependencyInjectionConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddTransient<IOperacaoTransient, Operacao>();
        builder.Services.AddScoped<IOperacaoScoped, Operacao>();
        builder.Services.AddSingleton<IOperacaoSingleton, Operacao>();
        builder.Services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty));
        builder.Services.AddTransient<OperacaoService>();

        return builder;
    }
}