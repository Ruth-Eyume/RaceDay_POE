using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDay.API.DTOs.CategoryDTOs;
using RaceDay.API.Models;

namespace RaceDay.API.Services
{
    public class CategoryService
    {
        private readonly RaceDayDBContext _context;

        public CategoryService(RaceDayDBContext context)
        {
            _context = context;
        }

        // Convert a Category model into a response DTO.
        private static CategoryResponseDTO MapToResponseDTO(Category category)
        {
            return new CategoryResponseDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        // GET: Retrieve all categories.
        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Set<Category>()
                .AsNoTracking()
                .ToListAsync();

            return categories.Select(MapToResponseDTO).ToList();
        }

        // GET: Retrieve a category by ID.
        public async Task<CategoryResponseDTO?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryID == id);

            if (category == null)
                return null;

            return MapToResponseDTO(category);
        }

        // POST: Create a new category.
        public async Task<CategoryResponseDTO> CreateCategoryAsync(
            CategoryCreateDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            _context.Set<Category>().Add(category);

            await _context.SaveChangesAsync();

            return MapToResponseDTO(category);
        }

        // PUT: Update an existing category.
        public async Task<CategoryResponseDTO?> UpdateCategoryAsync(
            int id, CategoryUpdateDTO dto)
        {
            var category = await _context.Set<Category>()
                .FirstOrDefaultAsync(c => c.CategoryID == id);

            if (category == null)
                return null;

            category.CategoryName = dto.CategoryName;
            category.Description = dto.Description;

            await _context.SaveChangesAsync();

            return MapToResponseDTO(category);
        }

        // DELETE: Delete a category.
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Set<Category>()
                .FirstOrDefaultAsync(c => c.CategoryID == id);

            if (category == null)
                return false;

            _context.Set<Category>().Remove(category);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}