using DataTables.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransPoster.MVC.Data.Products;
using TransPoster.MVC.Mvc;

namespace TransPoster.MVC.Controllers;

[AllowAnonymous]
public sealed class ProductsController : Controller
{
    public ViewResult Index() => View();

    public async Task<JsonResult> IndexTable(AjaxDataRequest param, [FromServices] ProductViewModelProccesser proccesser)
    {
        var viewModels = await proccesser.ProccessAsync(param);
        return this.JsonDefaultContract(viewModels);

    }
}
