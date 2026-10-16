using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class ParticipantServiceTests : IDisposable
    {
        private readonly RaceDayDBContext _context;
        private readonly ParticipantService _service;


    public ParticipantServiceTests()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new ParticipantService(_context);
        }

        // Test 1: An empty database returns an empty list.
        [Fact]
        public async Task GetAllParticipants_ShouldReturnEmptyList()
        {
            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 2: A nonexistent participant ID returns null.
        [Fact]
        public async Task GetParticipantById_ShouldReturnNullWhenNotFound()
        {
            var result = await _service.GetByIdAsync(999);

            Assert.Null(result);
        }

        // Test 3: Deleting a nonexistent participant returns false.
        [Fact]
        public async Task DeleteParticipant_ShouldReturnFalseWhenNotFound()
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
