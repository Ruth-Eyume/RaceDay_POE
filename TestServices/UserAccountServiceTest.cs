using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class UserAccountServiceTests : IDisposable
    {
        private readonly RaceDayDBContext _context;
        private readonly UserAccountService _service;


    public UserAccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new UserAccountService(_context);
        }

        // Test 1: An empty database returns an empty list.
        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList()
        {
            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 2: A nonexistent user account returns null.
        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullWhenNotFound()
        {
            var result = await _service.GetByIdAsync(999);

            Assert.Null(result);
        }

        // Test 3: Deleting a nonexistent user account returns false.
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalseWhenNotFound()
        {
            var result = await _service.DeleteAsync(999);

            Assert.False(result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
