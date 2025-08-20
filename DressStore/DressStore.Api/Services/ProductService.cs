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
            var products = await _context.Products
                .Select(p => new ProductDTO { Name = p.Name })
                .ToListAsync();

            return new Response<List<ProductDTO>>
            {
                data = products,
                success = true,
                message = Resource.OperationSuccessful
            };
        }

        public async Task<Response<ProductDTO>> GetProductByIdAsync(int id)
        {
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

            var dto = new ProductDTO { Name = product.Name };
            return new Response<ProductDTO>
            {
                data = dto,
                success = true,
                message = Resource.ProductFound
            };
        }

        public async Task<Response<ProductDTO>> CreateProductAsync(ProductDTO productDTO)
        {
            var product = new Product { Name = productDTO.Name };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var dto = new ProductDTO { Name = product.Name };
            return new Response<ProductDTO>
            {
                data = dto,
                success = true,
                message = Resource.ProductCreated
            };
        }

        public async Task<Response<ProductDTO>> UpdateProductAsync(int id, ProductDTO productDTO)
        {
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

            product.Name = productDTO.Name;
            await _context.SaveChangesAsync();

            var dto = new ProductDTO { Name = product.Name };
            return new Response<ProductDTO>
            {
                data = dto,
                success = true,
                message = Resource.ProductUpdated
            };
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
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new ProductDTO { Name = p.Name })
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return new Response<List<ProductDTO>>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductNotFound
                };
            }

                return new Response<List<ProductDTO>>
                {
                    data = products,
                    success = true,
                    message = Resource.OperationSuccessful
                };
        }
        public async Task<Response<List<ProductDTO>>> SearchProductsAsync(string searchTerm)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(searchTerm))
                .Select(p => new ProductDTO { Name = p.Name })
                .ToListAsync();

            if (products == null || !products.Any())
            {
                return new Response<List<ProductDTO>>
                {
                    data = null,
                    success = false,
                    message = Resource.ProductNotFound
                };
            }

            return new Response<List<ProductDTO>>
            {
                data = products,
                success = true,
                message = Resource.OperationSuccessful
            };
        }
    }
}
