using DataTables.Attributes;
using DataTables;
using System.ComponentModel.DataAnnotations;
using TransPoster.MVC.Models;

namespace TransPoster.MVC.Data.Products;

public sealed class ProductViewModel
{
    [Display(Name = "#")]
    [ColumnSettings(FilterType = FilterType.Text)]
    [SourceField]
    public int Id { get; set; }

    [Display(Name = "Name")]
    [ColumnSettings(FilterType = FilterType.Text)]
    [SourceField]
    public string Name { get; set; } = null!;

    [Display(Name = "Category")]
    [ColumnSettings(FilterType = FilterType.List, FilterItemsSource = "Filters/Categories")]
    [NavigationSource(nameof(Product.Category))]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Create Date")]
    [ColumnSettings(FilterType = FilterType.DateRange, DateFormat = KnownFormats.ShortDateFormat)]
    [SourceField]
    public DateTime CreateDate { get; set; }

    [Display(Name = "Is Active")]
    [ColumnSettings(FilterType = FilterType.Boolean)]
    [SourceField]
    public bool IsActive { get; set; }
}
