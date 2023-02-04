using DataTables;
using DataTables.Model;
using System.Linq.Expressions;
using TransPoster.MVC.Models;

namespace TransPoster.MVC.Data.Products;

public sealed class ProductViewModelProccesser : RemoteProccesserBase<Product, ProductViewModel>
{
    private readonly SampleDbContext db;

    public ProductViewModelProccesser(SampleDbContext db)
    {
        this.db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public Task<AjaxData> ProccessAsync(AjaxDataRequest param) => base.ProccessAsync(db.Products, param);

    protected override Expression<Func<Product, ProductViewModel>> Convert() => model => new ProductViewModel
    {
        Id = model.Id,
        Name = model.Name,
        CategoryName = model.Category.Name,
        IsActive = model.IsActive,
        CreateDate = model.CreateDate
    };

}