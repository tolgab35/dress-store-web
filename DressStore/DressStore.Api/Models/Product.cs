using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dress_store_web.Models;

[Index(nameof(Slug), IsUnique = true)]
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    [Precision(10,2)]
    public decimal Price { get; set; }                 // Temel liste fiyatı

    [Precision(10,2)]
    public decimal? CompareAtPrice { get; set; }       // Eski/etiket fiyatı (ops.)

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public int TotalStock => Variants?.Sum(v => v.Stock) ?? 0;
}
