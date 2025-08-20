using DressStore.Api.Models;
using DressStore.Api.Data;
using DressStore.Api.Dtos;
using DressStore.Api.Resources;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly AppDbContext _context;

        public ProductImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ProductImageDTO>>> GetAllProductImagesAsync()
        {
            var productImages = await _context.ProductImages
                .Select(pi => new ProductImageDTO
                {
                    Url = pi.Url,
                    ProductId = pi.ProductId,
                    IsPrimary = pi.IsPrimary,
                    SortOrder = pi.SortOrder
                })
                .ToListAsync();

            if (productImages == null || !productImages.Any())
            {
                return new Response<List<ProductImageDTO>>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductImageNotFound
                };
            }

            return new Response<List<ProductImageDTO>>
            {
                data = productImages,
                success = true,
                message = Resource.OperationSuccessful
            };
        }

        public async Task<Response<ProductImageDTO>> GetProductImageByIdAsync(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return new Response<ProductImageDTO>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductImageNotFound
                };
            }
            var dto = new ProductImageDTO
            {
                Url = productImage.Url,
                ProductId = productImage.ProductId,
                IsPrimary = productImage.IsPrimary,
                SortOrder = productImage.SortOrder
            };
            return new Response<ProductImageDTO>
            {
                data = dto,
                success = true,
                message = Resource.ProductImageFound
            };
        }

        public async Task<Response<ProductImageDTO>> CreateProductImageAsync(ProductImageDTO productImageDto)
        {
            var productImage = new ProductImage
            {
                Url = productImageDto.Url,
                ProductId = productImageDto.ProductId,
                IsPrimary = productImageDto.IsPrimary,
                SortOrder = productImageDto.SortOrder
            };
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();

            var dto = new ProductImageDTO
            {
                Url = productImage.Url,
                ProductId = productImage.ProductId,
                IsPrimary = productImage.IsPrimary,
                SortOrder = productImage.SortOrder
            };
            return new Response<ProductImageDTO>
            {
                data = dto,
                success = true,
                message = Resource.ProductImageCreated
            };
        }

        public async Task<Response<ProductImageDTO>> UpdateProductImageAsync(int id, ProductImageDTO productImageDto)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return new Response<ProductImageDTO>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductImageNotFound
                };
            }
            productImage.Url = productImageDto.Url;
            productImage.ProductId = productImageDto.ProductId;
            productImage.IsPrimary = productImageDto.IsPrimary;
            productImage.SortOrder = productImageDto.SortOrder;
            _context.ProductImages.Update(productImage);
            await _context.SaveChangesAsync();

            var dto = new ProductImageDTO
            {
                Url = productImage.Url,
                ProductId = productImage.ProductId,
                IsPrimary = productImage.IsPrimary,
                SortOrder = productImage.SortOrder
            };
            return new Response<ProductImageDTO>
            {
                data = dto,
                success = true,
                message = Resource.ProductImageUpdated
            };
        }

        public async Task<Response<bool>> DeleteProductImageAsync(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return new Response<bool>
                {
                    data = false,
                    success = false,
                    message = Resource.ProductImageNotFound
                };
            }
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return new Response<bool>
            {
                data = true,
                success = true,
                message = Resource.ProductImageDeleted
            };
        }
    }
}