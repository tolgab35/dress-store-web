using DressStore.Api.Models;
using DressStore.Api.Data;
using DressStore.Api.Dtos;
using DressStore.Api.Resources;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ProductDTO>>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Category) 
                    .Where(p => p.IsActive)    
                    .ToListAsync();

                var dtos = products.Select(p => new ProductDTO
                {
                    Id = p.Id,                    
                    Name = p.Name,
                    Slug = p.Slug,                
                    Description = p.Description,  
                    Price = p.Price,             
                    CompareAtPrice = p.CompareAtPrice, 
                    CategoryId = p.CategoryId,    
                    IsActive = p.IsActive
                }).ToList();

                return new Response<List<ProductDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<ProductDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductDTO>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return new Response<ProductDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.ProductNotFound
                    };
                }

                var dto = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Description = product.Description,
                    Price = product.Price,
                    CompareAtPrice = product.CompareAtPrice,
                    CategoryId = product.CategoryId,
                    IsActive = product.IsActive
                };

                return new Response<ProductDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductFound
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductDTO>> CreateProductAsync(ProductDTO productDTO)
        {
            try
            {
                if (!Validation.IsValidProductName(productDTO.Name, out string nameMessage))
                {
                    return new Response<ProductDTO>
                    {
                        data = null,
                        success = false,
                        message = nameMessage
                    };
                }

                if (!Validation.IsValidSlug(productDTO.Slug, out string slugMessage))
                {
                    return new Response<ProductDTO>
                    {
                        data = null,
                        success = false,
                        message = slugMessage
                    };
                }

                var product = new Product
                {
                    Name = productDTO.Name,
                    Slug = productDTO.Slug,
                    Description = productDTO.Description,
                    Price = productDTO.Price,
                    CompareAtPrice = productDTO.CompareAtPrice,
                    CategoryId = productDTO.CategoryId,
                    IsActive = productDTO.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Response DTO'da id dahil tüm alanları set etme kısmı:
                var dto = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Description = product.Description,
                    Price = product.Price,
                    CompareAtPrice = product.CompareAtPrice,
                    CategoryId = product.CategoryId,
                    IsActive = product.IsActive
                };

                return new Response<ProductDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductCreated
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<ProductDTO>> UpdateProductAsync(int id, ProductDTO productDTO)
        {
            try
            {
                if (!Validation.IsValidProductName(productDTO.Name, out string nameMessage))
                {
                    return new Response<ProductDTO>
                    {
                        data = null,
                        success = false,
                        message = nameMessage
                    };
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return new Response<ProductDTO>
                    {
                        data = null,
                        success = false,
                        message = Resource.ProductNotFound
                    };
                }

                // TÜM alanları günceller
                product.Name = productDTO.Name;
                product.Slug = productDTO.Slug;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.CompareAtPrice = productDTO.CompareAtPrice;
                product.CategoryId = productDTO.CategoryId;
                product.IsActive = productDTO.IsActive;
                product.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                // TÜM alanları döndürür
                var dto = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Description = product.Description,
                    Price = product.Price,
                    CompareAtPrice = product.CompareAtPrice,
                    CategoryId = product.CategoryId,
                    IsActive = product.IsActive
                };

                return new Response<ProductDTO>
                {
                    data = dto,
                    success = true,
                    message = Resource.ProductUpdated
                };
            }
            catch (Exception ex)
            {
                return new Response<ProductDTO>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }

        public async Task<Response<bool>> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new Response<bool>
                {
                    data = false,
                    success = false,
                    message = Resource.ProductNotFound
                };
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new Response<bool>
            {
                data = true,
                success = true,
                message = Resource.ProductDeleted
            };
        }
        public async Task<Response<List<ProductDTO>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.CategoryId == categoryId && p.IsActive)
                    .ToListAsync();

                if (!products.Any())
                {
                    return new Response<List<ProductDTO>>
                    {
                        data = new List<ProductDTO>(),  // Boş liste döndür, null değil
                        success = true,
                        message = "Bu kategoride ürün bulunamadı"
                    };
                }

                var dtos = products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.Description,
                    Price = p.Price,
                    CompareAtPrice = p.CompareAtPrice,
                    CategoryId = p.CategoryId,
                    IsActive = p.IsActive
                }).ToList();

                return new Response<List<ProductDTO>>
                {
                    data = dtos,
                    success = true,
                    message = Resource.OperationSuccessful
                };
            }
            catch (Exception ex)
            {
                return new Response<List<ProductDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }
        public async Task<Response<List<ProductDTO>>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetAllProductsAsync();  // Boşsa tümünü döndür
                }

                var products = await _context.Products
                    .Where(p => p.Name.Contains(searchTerm) && p.IsActive)
                    .ToListAsync();

                if (!products.Any())
                {
                    return new Response<List<ProductDTO>>
                    {
                        data = new List<ProductDTO>(),
                        success = true,
                        message = "Arama sonucu bulunamadı"
                    };
                }

                var dtos = products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.Description,
                    Price = p.Price,
                    CompareAtPrice = p.CompareAtPrice,
                    CategoryId = p.CategoryId,
                    IsActive = p.IsActive
                }).ToList();

                return new Response<List<ProductDTO>>
                {
                    data = dtos,
                    success = true,
                    message = $"{dtos.Count} ürün bulundu"
                };
            }
            catch (Exception ex)
            {
                return new Response<List<ProductDTO>>
                {
                    data = null,
                    success = false,
                    message = $"Hata: {ex.Message}"
                };
            }
        }
    }
}
