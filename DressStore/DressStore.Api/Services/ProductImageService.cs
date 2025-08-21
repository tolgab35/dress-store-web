using DressStore.Api.Dtos;
using DressStore.Api.Resources;
using DressStore.Api.Data;
using DressStore.Api.Models;
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
            try
            {
                var images = await _context.ProductImages.ToListAsync();
                var dtos = images.Select(pi => new ProductImageDTO
                {
                    Id = pi.Id,
                    ProductId = pi.ProductId,
                    Url = pi.Url,
                    IsPrimary = pi.IsPrimary,
                    SortOrder = pi.SortOrder
                }).ToList();

                return new Response<List<ProductImageDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<ProductImageDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductImageDTO>> GetProductImageByIdAsync(int id)
        {
            try
            {
                var image = await _context.ProductImages.FindAsync(id);
                if (image == null)
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
                    Id = image.Id,
                    ProductId = image.ProductId,
                    Url = image.Url,
                    IsPrimary = image.IsPrimary,
                    SortOrder = image.SortOrder
                };

                return new Response<ProductImageDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductImageFound
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductImageDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductImageDTO>> CreateProductImageAsync(ProductImageDTO dto)
        {
            try
            {
                var image = new ProductImage
                {
                    ProductId = dto.ProductId,
                    Url = dto.Url,
                    IsPrimary = dto.IsPrimary,
                    SortOrder = dto.SortOrder
                };
                _context.ProductImages.Add(image);
                await _context.SaveChangesAsync();

                dto.Id = image.Id;
                return new Response<ProductImageDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductImageCreated
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductImageDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductImageDTO>> UpdateProductImageAsync(int id, ProductImageDTO dto)
        {
            try
            {
                var image = await _context.ProductImages.FindAsync(id);
                if (image == null)
                {
                    return new Response<ProductImageDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.ProductImageNotFound
                    };
                }

                image.Url = dto.Url;
                image.IsPrimary = dto.IsPrimary;
                image.SortOrder = dto.SortOrder;
                image.ProductId = dto.ProductId;
                await _context.SaveChangesAsync();

                dto.Id = image.Id;
                return new Response<ProductImageDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductImageUpdated
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductImageDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<bool>> DeleteProductImageAsync(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);
            if (image == null)
            {
                return new Response<bool>
                {
                    data = false,
                    success = false,
                    message = Resource.ProductImageNotFound
                };
            }
            _context.ProductImages.Remove(image);
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