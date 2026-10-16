using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class EventServiceTests : IDisposable
    {
        private readonly RaceDayDBContext _context;
        private readonly EventService _service;

        public EventServiceTests()
        {
            // Create a separate temporary database for each test.
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new EventService(_context);
        }

        // Test 1: Retrieve all events when the database is empty.
        [Fact]
        public async Task GetAllEvents_ShouldReturnEmptyListWhenNoEventsExist()
        {
            // Act
            var result = await _service.GetAllEventsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 2: A nonexistent event ID should return null.
        [Fact]
        public async Task GetEventById_ShouldReturnNullWhenEventDoesNotExist()
        {
            // Act
            var result = await _service.GetEventByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        // Test 3: Deleting a nonexistent event should return false.
        [Fact]
        public async Task DeleteEvent_ShouldReturnFalseWhenEventDoesNotExist()
        {
            // Act
            var result = await _service.DeleteEventAsync(999);

            // Assert
            Assert.False(result);
        }

        // Dispose of the temporary database context.
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}