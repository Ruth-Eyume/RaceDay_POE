using Microsoft.AspNetCore.Mvc;
using RaceDay.API.DTOs.CategoryDTOs;
using RaceDay.API.Services;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return Ok(categories);
        }

        // GET: api/categories/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound(new { message = "Category not found." });

            return Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryResponseDTO>> CreateCategory(
            [FromBody] CategoryCreateDTO dto)
        {
            var category = await _categoryService.CreateCategoryAsync(dto);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = category.CategoryID },
                category);
        }

        // PUT: api/categories/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategory(
            int id, [FromBody] CategoryUpdateDTO dto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, dto);

            if (category == null)
                return NotFound(new { message = "Category not found." });

            return Ok(category);
        }

        // DELETE: api/categories/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);

            if (!deleted)
                return NotFound(new { message = "Category not found." });

            return NoContent();
        }
    }
}