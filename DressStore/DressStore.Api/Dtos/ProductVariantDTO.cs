using DressStore.Api.Models;
namespace DressStore.Api.Dtos;

public class ProductVariantDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public string Sku { get; set; } = null!;
    public int Stock { get; set; }
    public decimal? PriceOverride { get; set; }
}