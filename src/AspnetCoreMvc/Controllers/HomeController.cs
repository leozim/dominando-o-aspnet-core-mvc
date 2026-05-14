using System.Diagnostics;
using AspnetCoreMvc.Configuration;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvc.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AspnetCoreMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly ApiConfiguration _apiConfiguration;
    private readonly IStringLocalizer<HomeController> _localizer;

    public HomeController(
        ILogger<HomeController> logger, 
        IConfiguration configuration,
        IOptions<ApiConfiguration> apiConfiguration,
        IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _configuration = configuration;
        _apiConfiguration = apiConfiguration.Value;
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRIONMENT");

        var apiConfig = new ApiConfiguration();
        // uma forma de popular objetos fortemente tipados de arquivos de configurações
        _configuration.GetSection(ApiConfiguration.ConfigName).Bind(apiConfig);
        var secret = apiConfig.UserSecret;
        // outra forma
        var user = _configuration[$"{ApiConfiguration.ConfigName}:UserKey"];
        // através de options. melhor implementação
        var domain = _apiConfiguration.Domain;
        
        ViewData["Message"] = _localizer["Saudacao"];
        
        return View();
    }

    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)});

        return LocalRedirect(returnUrl);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
