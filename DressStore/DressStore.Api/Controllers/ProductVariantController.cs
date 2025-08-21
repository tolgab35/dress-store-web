using DressStore.Api.Models;
using DressStore.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DressStore.Api.Services;
using DressStore.Api.Dtos;

namespace DressStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductVariantController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductVariantService _service;

        public ProductVariantController(AppDbContext dbContext, IProductVariantService service)
        {
            _dbContext = dbContext;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllProductVariantsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetProductVariantByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductVariantDTO dto)
        {
            var result = await _service.CreateProductVariantAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductVariantDTO dto)
        {
            var result = await _service.UpdateProductVariantAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProductVariantAsync(id);
            return Ok(result);
        }

        // [HttpGet("by-product/{productId}")]
        //  public async Task<IActionResult> GetByProduct(int productId)
        //  {
        //      var result = await _service.GetProductVariantsByProductIdAsync(productId);
        //      return Ok(result);
        // }
        // bu metot temel işlemlerin dışında, bu yüzden şimdilik eklemiyorum.
        
    }
}
