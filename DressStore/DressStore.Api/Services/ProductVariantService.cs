using DressStore.Api.Dtos;
using DressStore.Api.Resources;
using DressStore.Api.Data;
using DressStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly AppDbContext _context;

        public ProductVariantService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ProductVariantDTO>>> GetAllProductVariantsAsync()
        {
            try
            {
                var variants = await _context.ProductVariants.ToListAsync();
                var dtos = variants.Select(v => new ProductVariantDTO
                {
                    Id = v.Id,
                    ProductId = v.ProductId,
                    Size = v.Size,
                    Color = v.Color,
                    Sku = v.Sku,
                    Stock = v.Stock,
                    PriceOverride = v.PriceOverride
                }).ToList();

                return new Response<List<ProductVariantDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<ProductVariantDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductVariantDTO>> GetProductVariantByIdAsync(int id)
        {
            try
            {
                var variant = await _context.ProductVariants.FindAsync(id);
                if (variant == null)
                {
                    return new Response<ProductVariantDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.ProductVariantNotFound
                    };
                }

                var dto = new ProductVariantDTO
                {
                    Id = variant.Id,
                    ProductId = variant.ProductId,
                    Size = variant.Size,
                    Color = variant.Color,
                    Sku = variant.Sku,
                    Stock = variant.Stock,
                    PriceOverride = variant.PriceOverride
                };

                return new Response<ProductVariantDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductVariantFound
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductVariantDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductVariantDTO>> CreateProductVariantAsync(ProductVariantDTO dto)
        {
            try
            {
                var variant = new ProductVariant
                {
                    ProductId = dto.ProductId,
                    Size = dto.Size,
                    Color = dto.Color,
                    Sku = dto.Sku,
                    Stock = dto.Stock,
                    PriceOverride = dto.PriceOverride
                };
                _context.ProductVariants.Add(variant);
                await _context.SaveChangesAsync();

                dto.Id = variant.Id;
                return new Response<ProductVariantDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductVariantCreated
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductVariantDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductVariantDTO>> UpdateProductVariantAsync(int id, ProductVariantDTO dto)
        {
            try
            {
                var variant = await _context.ProductVariants.FindAsync(id);
                if (variant == null)
                {
                    return new Response<ProductVariantDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.ProductVariantNotFound
                    };
                }

                variant.Size = dto.Size;
                variant.Color = dto.Color;
                variant.Sku = dto.Sku;
                variant.Stock = dto.Stock;
                variant.PriceOverride = dto.PriceOverride;
                variant.ProductId = dto.ProductId;
                await _context.SaveChangesAsync();

                dto.Id = variant.Id;
                return new Response<ProductVariantDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductVariantUpdated
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductVariantDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<bool>> DeleteProductVariantAsync(int id)
        {
            var variant = await _context.ProductVariants.FindAsync(id);
            if (variant == null)
            {
                return new Response<bool>
                {
                    data = false,
                    success = false,
                    message = Resource.ProductVariantNotFound
                };
            }
            _context.ProductVariants.Remove(variant);
            await _context.SaveChangesAsync();

            return new Response<bool>
            {
                data = true,
                success = true,
                message = Resource.ProductVariantDeleted
            };
        }
    }
}
