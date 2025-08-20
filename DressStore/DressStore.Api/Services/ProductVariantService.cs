using DressStore.Api.Models;
using DressStore.Api.Data;
using DressStore.Api.Dtos;
using DressStore.Api.Resources;
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
            var variants = await _context.ProductVariants
                .Select(v => new ProductVariantDTO
                {
                    ProductId = v.ProductId,
                    Size = v.Size,
                    Color = v.Color,
                    Sku = v.Sku,
                    Stock = v.Stock,
                    PriceOverride = v.PriceOverride
                })
                .ToListAsync();

            if (variants == null || !variants.Any())
            {
                return new Response<List<ProductVariantDTO>>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductVariantNotFound
                };
            }

            return new Response<List<ProductVariantDTO>>
            {
                data = variants,
                success = true,
                message = Resource.OperationSuccessful
            };
        }

        public async Task<Response<ProductVariantDTO>> GetProductVariantByIdAsync(int id)
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

        public async Task<Response<ProductVariantDTO>> CreateProductVariantAsync(ProductVariantDTO productVariantDto)
        {
            var variant = new ProductVariant
            {
                ProductId = productVariantDto.ProductId,
                Size = productVariantDto.Size,
                Color = productVariantDto.Color,
                Sku = productVariantDto.Sku,
                Stock = productVariantDto.Stock,
                PriceOverride = productVariantDto.PriceOverride
            };
            _context.ProductVariants.Add(variant);
            await _context.SaveChangesAsync();

            var dto = new ProductVariantDTO
            {
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
                message = Resource.ProductVariantCreated
            };
        }

        public async Task<Response<ProductVariantDTO>> UpdateProductVariantAsync(int id, ProductVariantDTO productVariantDto)
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

            variant.Size = productVariantDto.Size;
            variant.Color = productVariantDto.Color;
            variant.Sku = productVariantDto.Sku;
            variant.Stock = productVariantDto.Stock;
            variant.PriceOverride = productVariantDto.PriceOverride;
            variant.ProductId = productVariantDto.ProductId;

            _context.ProductVariants.Update(variant);
            await _context.SaveChangesAsync();

            var dto = new ProductVariantDTO
            {
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
                message = Resource.ProductVariantUpdated
            };
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

        public async Task<Response<List<ProductVariantDTO>>> GetProductVariantsByProductIdAsync(int productId)
        {
            var variants = await _context.ProductVariants
                .Where(v => v.ProductId == productId)
                .Select(v => new ProductVariantDTO
                {
                    ProductId = v.ProductId,
                    Size = v.Size,
                    Color = v.Color,
                    Sku = v.Sku,
                    Stock = v.Stock,
                    PriceOverride = v.PriceOverride
                })
                .ToListAsync();

            if (variants == null || !variants.Any())
            {
                return new Response<List<ProductVariantDTO>>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductVariantNotFound
                };
            }

            return new Response<List<ProductVariantDTO>>
            {
                data = variants,
                success = true,
                message = Resource.OperationSuccessful
            };
        }
    }
}
