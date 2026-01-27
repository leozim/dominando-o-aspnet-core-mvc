using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvc.Areas.Vendas.Controllers;

[Area("Produtos")]
public class CadastroController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Detalhes(int id)
    {
        return View("Index");
    }
}