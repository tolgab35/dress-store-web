using DressStore.Api.Models;
using DressStore.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DressStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImageController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductImageController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<List<ProductImage>> GetProductImages()
        {
            return await _dbContext.ProductImages.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage([FromBody] ProductImage productImage)
        {
            if (productImage == null)
            {
                return BadRequest("Product image cannot be null.");
            }
            _dbContext.ProductImages.Add(productImage);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductImages), new { id = productImage.Id }, productImage);
        }
        //aynı mantıkla çalışır.
    }
}
