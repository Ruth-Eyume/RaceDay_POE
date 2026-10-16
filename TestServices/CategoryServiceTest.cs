using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class CategoryServiceTests : IDisposable
    {
        private readonly RaceDayDBContext _context;
        private readonly CategoryService _service;

        public CategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new CategoryService(_context);
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturnCategories()
        {
            // Arrange
            _context.Categories.AddRange(
                new RaceDay.API.Models.Category
                {
                    CategoryName = "Running",
                    Description = "Road running events"
                },
                new RaceDay.API.Models.Category
                {
                    CategoryName = "Cycling",
                    Description = "Cycling events"
                });

            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllCategoriesAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturnNullWhenNotFound()
        {
            // Act
            var result = await _service.GetCategoryByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCategory_ShouldReturnFalseWhenNotFound()
        {
            // Act
            var result = await _service.DeleteCategoryAsync(999);

            // Assert
            Assert.False(result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}