using TransPoster.MVC.Interfaces;

namespace TransPoster.MVC.Models;

public class Category : IIdName
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}