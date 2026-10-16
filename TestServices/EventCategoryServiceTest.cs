using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class EventCategoryServiceTests : IDisposable
    {
        private readonly RaceDayDBContext _context;
        private readonly EventCategoryService _service;


    public EventCategoryServiceTests()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new EventCategoryService(_context);
        }

        // Test 1: An empty database returns an empty list.
        [Fact]
        public async Task GetAllAsync_ShouldReturnEmptyList()
        {
            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 2: A missing event category returns null.
        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullWhenNotFound()
        {
            var result = await _service.GetByIdAsync(999);

            Assert.Null(result);
        }

        // Test 3: Deleting a missing event category returns false.
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalseWhenNotFound()
        {
            var result = await _service.DeleteAsync(999);

            Assert.False(result);
        }

        // Test 4: Retrieve an existing event category.
        [Fact]
        public async Task GetByIdAsync_ShouldReturnExistingEventCategory()
        {
            // Arrange: Add an event and category first because
            // EventCategory references both of them.
            var route = new Route
            {
                RouteName = "Test Route",
                DistanceKm = 5.00m
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            var organiserAccount = new UserAccount
            {
                FirstName = "Test",
                LastName = "Organiser",
                Email = "organiser@test.com",
                PasswordHash = "test-hash",
                Role = "Organizer"
            };

            _context.UserAccounts.Add(organiserAccount);
            await _context.SaveChangesAsync();

            var organiser = new Organiser
            {
                UserAccountID = organiserAccount.UserAccountID,
                OrganisationName = "Test Organisation"
            };

            _context.Set<Organiser>().Add(organiser);
            await _context.SaveChangesAsync();

            var eventItem = new Event
            {
                EventName = "Test Running Event",
                Description = "Test event description",
                EventDate = DateTime.Today.AddDays(30),
                Location = "Cape Town",
                OrganiserID = organiser.OrganiserID,
                RouteID = route.RouteID
            };

            _context.Set<Event>().Add(eventItem);
            await _context.SaveChangesAsync();

            var category = new Category
            {
                CategoryName = "Test Running",
                Description = "Running category"
            };

            _context.Set<Category>().Add(category);
            await _context.SaveChangesAsync();

            var eventCategory = new EventCategory
            {
                EventID = eventItem.EventID,
                CategoryID = category.CategoryID,
                EntryFee = 100.00m,
                MaxParticipants = 50,
                CreatedAt = DateTime.Now
            };

            _context.Set<EventCategory>().Add(eventCategory);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetByIdAsync(
                eventCategory.EventCategoryID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(eventCategory.EventCategoryID, result.EventCategoryID);
            Assert.Equal(eventItem.EventID, result.EventID);
            Assert.Equal(category.CategoryID, result.CategoryID);
        }

        // Clean up after each test.
        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
