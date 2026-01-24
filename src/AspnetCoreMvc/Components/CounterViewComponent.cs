using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvc.Components;

public class CounterViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int modelCounter)
    {
        return View(modelCounter);
    }
}