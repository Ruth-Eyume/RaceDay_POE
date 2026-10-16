using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class EnrolmentServiceTests : IDisposable
    {
        private readonly RaceDayDBContext _context;
        private readonly EnrolmentService _service;


    public EnrolmentServiceTests()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new EnrolmentService(_context);
        }

        // Test 1: An empty database returns an empty list.
        [Fact]
        public async Task GetAllEnrolments_ShouldReturnEmptyList()
        {
            var result = await _service.GetAllEnrolmentsAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 2: A nonexistent enrolment returns null.
        [Fact]
        public async Task GetEnrolmentById_ShouldReturnNullWhenNotFound()
        {
            var result = await _service.GetEnrolmentByIdAsync(999);

            Assert.Null(result);
        }

        // Test 3: Deleting a nonexistent enrolment returns false.
        [Fact]
        public async Task DeleteEnrolment_ShouldReturnFalseWhenNotFound()
        {
            var result = await _service.DeleteEnrolmentAsync(999);

            Assert.False(result);
        }

        // Clean up the temporary database context.
        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
