using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Models;

[Index(nameof(Sku), IsUnique = true)]
public class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string? Size { get; set; }   // örn: S,M,L,XL
    public string? Color { get; set; }  // örn: Red, Black

    public string Sku { get; set; } = null!;
    public int Stock { get; set; }

    [Precision(10,2)]
    public decimal? PriceOverride { get; set; } // opsiyonel
}
