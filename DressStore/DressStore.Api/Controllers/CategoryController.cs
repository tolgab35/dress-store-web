using Microsoft.AspNetCore.Mvc;
using DressStore.Api.Services;
using DressStore.Api.Dtos;

namespace DressStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetCategoryByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDTO dto)
        {
            var result = await _service.CreateCategoryAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO dto)
        {
            var result = await _service.UpdateCategoryAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term)
        {
            var result = await _service.SearchCategoriesAsync(term);
            return Ok(result);
        }

        [HttpGet("by-product/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var result = await _service.GetCategoriesByProductIdAsync(productId);
            return Ok(result);
        }
    }
}