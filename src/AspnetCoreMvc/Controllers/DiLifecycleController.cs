using AspnetCoreMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvc.Controllers;

[Route("api/teste-di")]
public class DiLifecycleController : Controller
{
    public OperacaoService OperacaoService { get; }
    public OperacaoService OperacaoService2 { get; set; }

    public DiLifecycleController(OperacaoService operacaoService, OperacaoService operacaoService2)
    {
        OperacaoService = operacaoService;
        OperacaoService2 = operacaoService2;
    }
    
    // GET
    public string Index()
    {
        return
            "Primeira instância: " + Environment.NewLine +
            OperacaoService.Transient.OperacaoId + Environment.NewLine +
            OperacaoService.Scoped.OperacaoId + Environment.NewLine +
            OperacaoService.Singleton.OperacaoId + Environment.NewLine +
            OperacaoService.SingletonInstance.OperacaoId + Environment.NewLine +

            Environment.NewLine +
            Environment.NewLine +

            "Segunda instância: " + Environment.NewLine +
            OperacaoService2.Transient.OperacaoId + Environment.NewLine +
            OperacaoService2.Scoped.OperacaoId + Environment.NewLine +
            OperacaoService2.Singleton.OperacaoId + Environment.NewLine +
            OperacaoService2.SingletonInstance.OperacaoId + Environment.NewLine;
    }
}