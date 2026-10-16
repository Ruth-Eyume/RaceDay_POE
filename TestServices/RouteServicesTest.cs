using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.DTOs.RouteDTOs;
using RaceDay.API.Services;
using RaceDayAPI.Data;
using Xunit;

namespace RaceDay.Tests.Services
{
    public class RouteServiceTests : IDisposable

    {
        private readonly RaceDayDBContext _context;
        private readonly RouteService _service;

        // Create a separate temporary database for each test.
        public RouteServiceTests()
        {
            var options = new DbContextOptionsBuilder<RaceDayDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new RaceDayDBContext(options);
            _service = new RouteService(_context);
        }

        // Test 1: A route can be created successfully.
        [Fact]
        public async Task CreateRoute_ShouldSaveRoute()
        {
            // Arrange
            var dto = new RouteCreateDTO
            {
                RouteName = "Cape Town Road Route",
                DistanceKm = 10.00m,
                StartPoint = "Cape Town Stadium",
                EndPoint = "Green Point",
                RouteDescription = "A 10 km running route",
                MapUrl = null,
                Latitude = null,
                Longitude = null
            };

            // Act
            var result = await _service.CreateRouteAsync(dto);

            // Assert
            Assert.True(result.RouteID > 0);
            Assert.Equal("Cape Town Road Route", result.RouteName);
            Assert.Equal(10.00m, result.DistanceKm);

            var savedRoute = await _context.Routes
                .FirstOrDefaultAsync(r => r.RouteID == result.RouteID);

            Assert.NotNull(savedRoute);
        }

        // Test 2: All routes can be retrieved.
        [Fact]
        public async Task GetAllRoutes_ShouldReturnRoutes()
        {
            // Arrange
            await _service.CreateRouteAsync(new RouteCreateDTO
            {
                RouteName = "Route One",
                DistanceKm = 5.00m
            });

            await _service.CreateRouteAsync(new RouteCreateDTO
            {
                RouteName = "Route Two",
                DistanceKm = 10.00m
            });

            // Act
            var routes = await _service.GetAllRoutesAsync();

            // Assert
            Assert.Equal(2, routes.Count);
        }

        // Test 3: A route can be retrieved by its ID.
        [Fact]
        public async Task GetRouteById_ShouldReturnCorrectRoute()
        {
            // Arrange
            var createdRoute = await _service.CreateRouteAsync(
                new RouteCreateDTO
                {
                    RouteName = "Test Route",
                    DistanceKm = 15.00m
                });

            // Act
            var result = await _service.GetRouteByIdAsync(
                createdRoute.RouteID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdRoute.RouteID, result.RouteID);
            Assert.Equal("Test Route", result.RouteName);
        }

        // Test 4: A missing route ID should return null.
        [Fact]
        public async Task GetRouteById_ShouldReturnNullWhenRouteDoesNotExist()
        {
            // Act
            var result = await _service.GetRouteByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        // Test 5: An existing route can be updated.
        [Fact]
        public async Task UpdateRoute_ShouldUpdateRouteDetails()
        {
            // Arrange
            var createdRoute = await _service.CreateRouteAsync(
                new RouteCreateDTO
                {
                    RouteName = "Old Route Name",
                    DistanceKm = 5.00m
                });

            var updateDto = new RouteUpdateDTO
            {
                RouteName = "Updated Route Name",
                DistanceKm = 12.00m,
                StartPoint = "Start Point",
                EndPoint = "End Point",
                RouteDescription = "Updated route description",
                MapUrl = null,
                Latitude = null,
                Longitude = null
            };

            // Act
            var result = await _service.UpdateRouteAsync(
                createdRoute.RouteID, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Route Name", result.RouteName);
            Assert.Equal(12.00m, result.DistanceKm);
        }

        // Test 6: Updating a missing route should return null.
        [Fact]
        public async Task UpdateRoute_ShouldReturnNullWhenRouteDoesNotExist()
        {
            // Arrange
            var updateDto = new RouteUpdateDTO
            {
                RouteName = "Updated Route",
                DistanceKm = 8.00m
            };

            // Act
            var result = await _service.UpdateRouteAsync(999, updateDto);

            // Assert
            Assert.Null(result);
        }

        // Test 7: An existing route can be deleted.
        [Fact]
        public async Task DeleteRoute_ShouldRemoveRoute()
        {
            // Arrange
            var createdRoute = await _service.CreateRouteAsync(
                new RouteCreateDTO
                {
                    RouteName = "Route to Delete",
                    DistanceKm = 7.00m
                });

            // Act
            var result = await _service.DeleteRouteAsync(
                createdRoute.RouteID);

            // Assert
            Assert.True(result);

            var deletedRoute = await _context.Routes
                .FirstOrDefaultAsync(r =>
                    r.RouteID == createdRoute.RouteID);

            Assert.Null(deletedRoute);
        }

        // Test 8: Deleting a missing route should return false.
        [Fact]
        public async Task DeleteRoute_ShouldReturnFalseWhenRouteDoesNotExist()
        {
            // Act
            var result = await _service.DeleteRouteAsync(999);

            // Assert
            Assert.False(result);
        }

        // Release the temporary database context after each test.
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
